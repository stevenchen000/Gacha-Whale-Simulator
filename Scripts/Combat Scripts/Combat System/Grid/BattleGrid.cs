using Godot;
using System;
using Godot.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CombatSystem {
	public partial class BattleGrid : Node2D
	{
		private Array<GridSpace> battleSpaces;
		private Godot.Collections.Dictionary<Vector2I, GridSpace> gridPositions;
		private Array<GridSpace> borderSpaces;

		[Export] private PackedScene spaceScene;
		[Export] private Vector2 startingPoint;
		[Export] private Vector2 horizontalOffset;
		[Export] private Vector2 verticalOffset;

		[Export] private int horizontalGrids = 7;
		[Export] private int verticalGrids = 3;

        public MovementData CurrentMovementData { get; private set; }
        public TargetingData CurrentTargetingData { get; private set; }
		public TargetingSelection CurrentSelection { get; private set; }


		private Godot.Collections.Dictionary<BattleCharacter, GridSpace> occupiedSpaces;
		private Godot.Collections.Dictionary<GridSpace, BattleCharacter> occupiedSpacesReverse;



		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			
		}

		public void InitGrid(int horizontal, int vertical, BattleState state)
        {
			horizontalGrids = horizontal;
			verticalGrids = vertical;
            occupiedSpaces = new Godot.Collections.Dictionary<BattleCharacter, GridSpace>();
			occupiedSpacesReverse = new Godot.Collections.Dictionary<GridSpace, BattleCharacter>();
            InitBattleSpaces();
            SetAllSpacesToDefault();
        }



		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
            
        }

		


		/*****************
		 * Space State Update
		 * **************/

		public void SetWalkableData(MovementData data)
		{
			CurrentMovementData = data;
			UpdateSpaceState();
		}


		public void SetTargetingData(TargetingData data)
		{
			CurrentTargetingData = data;
            UpdateSpaceState();
        }

		public void SetTargetingSelection(TargetingSelection selection)
		{
			CurrentSelection = selection;
            UpdateSpaceState();
        }

		private void UpdateSpaceState()
		{
			ResetSpaces();
			UpdateMovementData(CurrentMovementData);
			UpdateTargetingData(CurrentTargetingData);
			UpdateTargetSelection(CurrentSelection);
		}

		private void UpdateMovementData(MovementData moveData)
		{
			if (moveData != null)
			{
				var coordinates = moveData.WalkableSpaces;
				foreach (var coords in coordinates)
				{
					var space = GetSpaceFromCoords(coords);
					space.SetWalkable(true);
				}
			}
		}

		/******************
		 * Targeting Data
		 * ****************/

		private void UpdateTargetingData(TargetingData targetingData)
		{
			if (targetingData != null)
			{
				var style = targetingData.SelectionStyle;
				switch (style)
				{
					case TargetSelectionStyle.SelectSpace: //make all selectable spaces yellow
                        _UpdateTargetDataSpace(targetingData);
                        break;
					case TargetSelectionStyle.SelectDirection: //make all possible spaces yellow
						_UpdateTargetDataDirection(targetingData);
                        break;
					case TargetSelectionStyle.None: //make all spaces red (target already selected)
						_UpdateTargetDataNone(targetingData);
                        break;
				}
			}
		}

		private void _UpdateTargetDataSpace(TargetingData targetingData)
		{
            var spaceCoords = targetingData._spaceDict.Keys;
            foreach (var coords in spaceCoords)
            {
                var space = GetSpaceFromCoords(coords);
                space.SetCanSelect(true);
            }
        }

		private void _UpdateTargetDataDirection(TargetingData targetingData)
		{
            var directions = targetingData.GetValidDirections(this);
            foreach (var direction in directions)
            {
                var directionOffset = BattleConstants.GetDirectionOffset(direction);
                var characterPos = targetingData.Caster.currPosition;
                var space = GetSpaceFromCoords(characterPos + directionOffset);
                space.SetCanSelect(true);
                space.SetSelectionHasDirection(direction);
            }
        }

		private void _UpdateTargetDataNone(TargetingData targetingData)
		{
            var targetCoords = targetingData._spaces;
            foreach (var coords in targetCoords)
            {
                var space = GetSpaceFromCoords(coords);
                space.SetHasSelected(true);
            }
			CurrentSelection = new TargetingSelection();
        }


		/********************
		 * Target Selection
		 * ****************/

		private void UpdateTargetSelection(TargetingSelection targetSelection)
		{
			if(targetSelection != null)
			{
				var style = targetSelection.Style;
				switch (style)
				{
					case TargetSelectionStyle.SelectSpace:
						_UpdateSpaceSelection(targetSelection);
                        break;
					case TargetSelectionStyle.SelectDirection:
						_UpdateDirectionSelection(targetSelection);
                        break;
					case TargetSelectionStyle.None: //do nothing, already shown in UpdateTargetingData
						break;
				}
			}
		}

		private void _UpdateSpaceSelection(TargetingSelection selection)
		{
			if (CurrentTargetingData == null) return;
            var selectedSpace = selection.SelectedSpace;
            var spaceTargets = CurrentTargetingData._spaceDict[selectedSpace];
            foreach (var coords in spaceTargets)
            {
                var space = GetSpaceFromCoords(coords);
                space?.SetHasSelected(true);
            }
        }

		private void _UpdateDirectionSelection(TargetingSelection selection)
		{
            var selectedDirection = selection.SelectedDirection;
            var directionSpaces = CurrentTargetingData._directionDict[selectedDirection];
            foreach (var coords in directionSpaces)
            {
                var space = GetSpaceFromCoords(coords);
				space?.SetHasSelected(true);
            }
        }


		/********************
		 * Misc
		 * ****************/

        public void ResetSpaces()
        {
            foreach (var space in battleSpaces)
                space.ResetSpace();
        }

		public void ResetGrid()
		{
			CurrentMovementData = null;
			CurrentTargetingData = null;
			CurrentSelection = null;
			ResetSpaces();
		}

		public bool SpaceIsInGrid(Vector2I coords)
		{
			return GetSpaceFromCoords(coords) != null;
		}

        /******************
		 * Get Space
		 * ***************/

        #region Get Space

        public GridSpace GetSpaceFromCoords(int x, int y)
		{
			Vector2I coords = new Vector2I(x, y);
			GridSpace result = null;

			if (gridPositions.ContainsKey(coords))
			{
				result = gridPositions[coords];
			}

			return result;
		}
		public GridSpace GetSpaceFromCoords(Vector2I pos)
        {
			int x = pos.X;
			int y = pos.Y;
			return GetSpaceFromCoords(x, y);
        }


		public bool CoordinatesInGrid(Vector2I coords)
		{
			var space = GetSpaceFromCoords(coords);
			return space != null;
		}

		#endregion



		private Array<BattleCharacter> PositionsToTargets(Array<Vector2I> coords)
        {
			var result = new Array<BattleCharacter>();
			foreach(var coord in coords)
            {
				var space = GetSpaceFromCoords(coord);
				var target = space.CharacterOnSpace;
				if (target != null) result.Add(target);
            }
			return result;
        }

		private Array<BattleCharacter> GetValidTarget(BattleManager battle, Array<BattleCharacter> targets, BattleCharacter character)
        {
			var result = new Array<BattleCharacter>();

			foreach(var target in targets)
            {
				bool isEnemy = !battle.IsInSameParty(target, character);
				if (isEnemy) result.Add(target);
            }

			return result;
        }

		/******************
		 * Occupy and unoccupy spaces
		 * ****************/

		public void OccupySpace(BattleCharacter character, Vector2I coords)
		{
			var space = GetSpaceFromCoords(coords);

			UnoccupySpace(character);
			occupiedSpaces.Add(character, space);
			occupiedSpacesReverse.Add(space, character);
		}

		public void UnoccupySpace(BattleCharacter character)
		{
            if (occupiedSpaces.ContainsKey(character))
            {
                var prevSpace = occupiedSpaces[character];
				occupiedSpaces.Remove(character);
                occupiedSpacesReverse.Remove(prevSpace);
            }
        }

		public void UnoccupySpace(Vector2I coords)
		{
			var space = GetSpaceFromCoords(coords);
            if (occupiedSpacesReverse.ContainsKey(space))
            {
                var prevChar = occupiedSpacesReverse[space];
                occupiedSpaces.Remove(prevChar);
                occupiedSpacesReverse.Remove(space);
            }
        }

		public BattleCharacter GetCharacterOnSpace(Vector2I coords)
		{
			var space = GetSpaceFromCoords(coords);
			BattleCharacter result = null;
			if (occupiedSpacesReverse.ContainsKey(space))
			{
				result = occupiedSpacesReverse[space];
			}

			return result;
		}

		public GridSpace GetSpaceOccupiedByCharacter(BattleCharacter character)
		{
			GridSpace result = null;
            if (occupiedSpaces.ContainsKey(character))
            {
                result = occupiedSpaces[character];
            }

            return result;
        }


		/*********************
		 * Walkable Spaces
		 * *****************/

		public MovementData GetAllWalkableAreas(BattleCharacter character)
        {
			var position = character.turnStartPosition;//GetNearestSpaceToCharacter(character);
            int movement = character.Stats.GetStat(StatNames.Movement);

            var walkableSpaces = new Array<Vector2I>();
            TraverseMapRecursive(walkableSpaces, position, movement, character);
			RemoveAllOccupiedSpaces(character, walkableSpaces);

			var result = new MovementData(character, walkableSpaces);

            return result;
		}
		public bool CheckIfSpaceIsWalkable(Vector2I position)
        {
			return CurrentMovementData.WalkableSpaces.Contains(position);
        }
		public bool CheckIfSpaceIsWalkable(GridSpace space)
        {
			return space.IsWalkable;
        }


		private void TraverseMapRecursive(Array<Vector2I> ReachableSpaces, 
										  Vector2I currSpace, 
										  int movement, 
										  BattleCharacter character)
		{
			var space = GetSpaceFromCoords(currSpace);
			if (space == null) return;

			var characterOnSpace = space.CharacterOnSpace;
			//exit if enemy on space
			if(characterOnSpace != null && characterOnSpace.IsEnemy(character))
			{
				return;
			}

			//add space
            if (!ReachableSpaces.Contains(currSpace))
            {
                ReachableSpaces.Add(currSpace);
            }

			//traverse all other spaces
            if (movement > 0)
			{
				var left = currSpace + new Vector2I(-1, 0);
				var right = currSpace + new Vector2I(1, 0);
				var up = currSpace + new Vector2I(0, 1);
				var down = currSpace + new Vector2I(0, -1);

				TraverseMapRecursive(ReachableSpaces, left, movement - 1, character);
				TraverseMapRecursive(ReachableSpaces, down, movement - 1, character);
				TraverseMapRecursive(ReachableSpaces, up, movement - 1, character);
				TraverseMapRecursive(ReachableSpaces, right, movement - 1, character);
			}
		}



		/*******************
		 * Change Space State
		 * ****************/

		public void SetAllSpacesToDefault()
		{
			foreach (var space in battleSpaces)
			{
				space.ResetSpace();
			}
		}
		public void SetSpacesWalkable(BattleCharacter character)
		{
			var positions = character.WalkableSpaces;

			foreach (var position in positions)
			{
				var space = GetSpaceFromCoords(position);
				space.SetWalkable(true);
			}
		}


		/****************
		* Helper Functions
		* **************/

		private Array<Vector2I> GetWalkableSpacesIgnoringObstacles(Vector2I position, int movableSpaces)
        {
			int x = position.X;
			int y = position.Y;
			int lowerX = x - movableSpaces;
			int upperX = x + movableSpaces;
			int lowerY = y - movableSpaces;
			int upperY = y + movableSpaces;

			var walkableSpaces = new Array<Vector2I>();

			for (int i = lowerX; i <= upperX; i++)
			{
				for (int j = lowerY; j <= upperY; j++)
				{
					int distX = Math.Abs(x - i);
					int distY = Math.Abs(y - j);
					int distance = distX + distY;
					var space = GetSpaceFromCoords(i, j);
					if (space == null) continue;
					if(distance <= movableSpaces)
						walkableSpaces.Add(new Vector2I(i, j));
				}
			}

			return walkableSpaces;
        }

		private Array<Vector2I> RemoveSpacesWithEnemies(BattleCharacter character, Array<Vector2I> spaces)
        {
			Array<Vector2I> result = new Array<Vector2I>();

            foreach (var coord in spaces)
			{
				var space = GetSpaceFromCoords(coord);
				var spaceCharacter = space.CharacterOnSpace;
				if (spaceCharacter != null)
				{
					if(!character.IsEnemy(spaceCharacter))
                    {
						result.Add(coord);
                    }
                }
                else
                {
					result.Add(coord);
                }
			}

			return result;
        }

		private Array<Vector2I> RemoveDisconnectedSpaces(Array<Vector2I> spaces, BattleCharacter character)
        {
			var currPos = character.turnStartPosition;
			var result = new Array<Vector2I>();
			result.Add(currPos);

			AddAllWalkableNeighbors(currPos, spaces, result);
			return result;
        }

		private Array<Vector2I> RemoveAllOccupiedSpaces(BattleCharacter character, Array<Vector2I> spaces)
		{
			var result = new Array<Vector2I>();

			foreach(var coords in spaces)
			{
				var space = GetSpaceFromCoords(coords);
				var spaceCharacter = space.CharacterOnSpace;

				if(spaceCharacter == null || spaceCharacter == character)
				{
					result.Add(coords);
				}
			}

			return result;
		}

		private void AddAllWalkableNeighbors(Vector2I currSpace, Array<Vector2I> originalSpaces, Array<Vector2I> finalSpaces)
        {
			int x = currSpace.X;
			int y = currSpace.Y;
			//left neighbor
			var left = new Vector2I(x-1, y);
			var right = new Vector2I(x + 1, y);
			var up = new Vector2I(x, y + 1);
			var down = new Vector2I(x, y - 1);

			if(SpaceNeedsToBeChecked(left, originalSpaces, finalSpaces))
            {
				finalSpaces.Add(left);
				AddAllWalkableNeighbors(left, originalSpaces, finalSpaces);
            }
			if(SpaceNeedsToBeChecked(right, originalSpaces, finalSpaces))
            {
                finalSpaces.Add(right);
				AddAllWalkableNeighbors(right, originalSpaces, finalSpaces);
			}
			if(SpaceNeedsToBeChecked(up, originalSpaces, finalSpaces))
            {
                finalSpaces.Add(up);
				AddAllWalkableNeighbors(up, originalSpaces, finalSpaces);
			}
			if(SpaceNeedsToBeChecked(down, originalSpaces, finalSpaces))
            {
                finalSpaces.Add(down);
				AddAllWalkableNeighbors(down, originalSpaces, finalSpaces);
			}

		}

		private bool SpaceNeedsToBeChecked(Vector2I space, Array<Vector2I> originalSpaces, Array<Vector2I> finalSpaces)
        {
			return originalSpaces.Contains(space) && !finalSpaces.Contains(space);

		}

		private void InitBattleSpaces()
		{
			battleSpaces = new Array<GridSpace>();
			gridPositions = new Godot.Collections.Dictionary<Vector2I, GridSpace>();

			for (int i = 0; i < horizontalGrids; i++)
			{
				for (int j = 0; j < verticalGrids; j++)
				{
					var space = InstantiateBattleSpace();
					SetupGridspacePosition(space, i, j);
					battleSpaces.Add(space);
					var position = new Vector2I(i, j);
					gridPositions[position] = space;
					space.InitSpace(position);
				}
			}
		}

		private void InitBoundarySpaces()
        {
			borderSpaces = new Array<GridSpace>();

			for(int i = -1; i < horizontalGrids + 1; i++)
            {
				CreateBoundarySpace(i, -1);
				CreateBoundarySpace(i, verticalGrids);
            }

			for(int j = 0; j < verticalGrids; j++)
            {
				CreateBoundarySpace(-1, j);
				CreateBoundarySpace(horizontalGrids, j);
            }
        }

		private void CreateBoundarySpace(int x, int y)
        {
			var space = InstantiateBattleSpace();
			SetupGridspacePosition(space, x, y);
			//space.SetState(GridState.BOUNDARY);
			borderSpaces.Add(space);
			
		}

		private GridSpace InstantiateBattleSpace()
        {
			var space = (GridSpace)Utils.InstantiateCopy(spaceScene);
			AddChild(space);
			
			return space;
        }

		private void SetupGridspacePosition(GridSpace space, int x, int y)
        {
			Vector2 position = startingPoint;
			position += x * horizontalOffset;
			position += y * verticalOffset;
			space.Position = position;
		}




		public List<GridSpace> GetSortedListByDiagonal()
		{
			var list = new List<GridSpace>();
			list.AddRange(battleSpaces);
			return list.OrderBy(space => (space.Coords.X + space.Coords.Y)).ThenByDescending(space => space.Coords.Y).ToList();
		}
	}
}
