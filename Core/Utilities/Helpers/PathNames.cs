using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Helpers
{
    public static class PathNames
    {
        public static string BaseName = @"\WebApi\wwwroot\";
        public static string CarImage = @"Uploads\CarImages\";
        public static string AddCarImage = BaseName + CarImage;
        public static string CarDefaultImage = CarImage + "defaultImage.png";
    }
}
