using Godot;
using System;

namespace GachaSystem
{
    public partial class GachaBannerUI : Node
    {
        [Export] private GachaBanner banner;
        [Export] private TextureRect bannerIcon;
        
        public void SetupBanner(GachaBanner newBanner)
        {
            banner = newBanner;
        }

        public void SinglePull()
        {
            //should be a confirmation screen first
            banner.GetSinglePull();
        }

        public void MultiPull()
        {
            var image = new Image();
            //should be a confirmation screen first
            banner.GetMultiPull();
        }
    }
}