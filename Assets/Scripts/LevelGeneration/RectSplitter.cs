using UnityEngine;

namespace LevelGeneration
{
    public static class RectSplitter
    {
        private static float _minSplitSize = 0.3f;
        private static float _maxSplitSize = 0.7f;
        
        public static RectInt[] SplitRect(RectInt rect)
        {
            var relativeWidth = (float)rect.width / rect.height;
            if (relativeWidth > 2f)
                return HorizontalSplitRect(rect); 
            if (relativeWidth < 0.5f)
                return VerticalSplitRect(rect);
                    
            bool isHorizontalSplit = Random.Range(1, 100) < 50;
            return isHorizontalSplit ? HorizontalSplitRect(rect) : VerticalSplitRect(rect);
        }
        
        private static RectInt[] HorizontalSplitRect(RectInt rect) 
        {
            RectInt[] newRects = new RectInt[2];
            int firstRectWidth = GetSplitSize(rect.width);

            newRects[0] = new RectInt(rect.x, rect.y, firstRectWidth, rect.height);
            firstRectWidth += 1;
            newRects[1] = new RectInt(rect.x + firstRectWidth, rect.y, rect.width - firstRectWidth, rect.height);

            return newRects;
        }
        
        private static RectInt[] VerticalSplitRect(RectInt rect)
        {
            var newRects = new RectInt[2];
            int firstRectHeight = GetSplitSize(rect.height);

            newRects[0] = new RectInt(rect.x, rect.y, rect.width, firstRectHeight);
            firstRectHeight += 1;
            newRects[1] = new RectInt(rect.x, rect.y + firstRectHeight, rect.width, rect.height - firstRectHeight);

            return newRects;
        }

        private static int GetSplitSize(int size) 
        {
            return (int)Random.Range(size * _minSplitSize, size * _maxSplitSize);
        }
    }
}
