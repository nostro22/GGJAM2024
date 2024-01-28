using UnityEngine;

    public class UtilsPlayer 
    {
        static UtilsPlayer()
        {
            
        }
        
        
        public static Color GetColoByIndex(int index)
        {
            Color newColor;
            if (index == 1)
            {
                newColor = Color.cyan;
            }
            else if (index == 2)
            {
                newColor = Color.red;
            }
            else if (index == 3)
            {
                newColor = Color.green;
            }
            else
            {
                newColor = Color.yellow;
            }

            return newColor;
        }
    }
