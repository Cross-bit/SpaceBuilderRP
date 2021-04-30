using UnityEngine;
namespace Assets.Scripts.GameCore.API.Extensions
{
    public static class Vector3Extensions
    {

        /// <summary>
        /// Odečte konstantu od Vector3
        /// </summary>
        /// <param name="vec3"> Základní vec3. </param>
        /// /// <param name="val"> Hodnota pro odečtení. </param>
        /// <returns></returns>
        public static Vector3 AddScalarToVector(this Vector3 vec3, float val)
        {
            if (vec3 == null)
            {
                Debug.LogAssertion("Nulový VEKTOR!!!");
                return new Vector3();
            }

            if (val == 0)
            {
                Debug.LogAssertion("Od vec3 se odečítá 0 hodnota!");
            }

            return new Vector3(vec3.x + val, vec3.y + val, vec3.z + val);
        }

        /// <summary>
        /// ABS všech values.
        /// </summary>
        /// <param name="vec3"> Základní vec3. </param>
        /// <returns></returns>
        public static Vector3 ABS(this Vector3 vec3)
        {
            if (vec3 == null)
            {
                Debug.LogAssertion("Nulový VEKTOR!!!");
                return new Vector3();
            }

            return new Vector3(Mathf.Abs(vec3.x), Mathf.Abs(vec3.y), Mathf.Abs(vec3.z));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentPos"></param>
        /// <param roundTo=""> Na kolik desetinných míst (defaultně: 2)</param>
        /// <returns></returns>
        public static Vector3 GetRoundedPosition(this Vector3 currentPos, short roundTo = 2)
        {

            Vector3 num = new Vector3(
                Settings.RoundTo(currentPos.x, roundTo),
                Settings.RoundTo(currentPos.y, roundTo),
                Settings.RoundTo(currentPos.z, roundTo)
            );

            // Vector3 res = new Vector3


            return num;
        }

        /// <summary>
        /// Vrací Vector3 souřadnice orotované okolo osy z.
        /// </summary>
        /// <param name="vec3">Původní Vecor3 souřadnice.</param>
        /// <param name="degrees">O kolik chceme otáčet. (Ve stupních) </param>
        /// <returns></returns>
        public static Vector3 RotateOnY(this Vector3 vec3, float degrees)
        {
            // TODO: Možná jednou v budoucnu i rotace po ostatních osách, teď jen po z
            Vector2 c_rotated = Settings.Rotate2D(new Vector2(vec3.x, vec3.z), degrees);

            Vector3 vec3_r = new Vector3(c_rotated.x, vec3.y, c_rotated.y);

            return vec3_r;
        }

    }

}
