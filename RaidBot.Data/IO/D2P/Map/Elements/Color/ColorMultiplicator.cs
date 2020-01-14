using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Data.IO.D2P.Map.Elements.Color
{
        [Serializable()]
    public class ColorMultiplicator
    {

        #region Delcarations

        public double red { get; set; }
        public double green { get; set; }
        public double blue { get; set; }
        private Boolean isOne { get; set; }

        #endregion

        #region Constructeur

        public ColorMultiplicator(int _red, int _green, int _blue, Boolean _isOne = false)
        {
            red = _red;
            green = _green;
            blue = _blue;
            if (!_isOne && _red + _green + _blue == 0)
            {
                this.isOne = true;
            }
        }

        #endregion

        #region Methodes Publiques

        public static double clamp(double _red, double _green, double _blue)
        {
            if (_red > _blue)
            {
                return _blue;
            }
            if (_red < _green)
            {
                return _green;
            }
            return _red;
        }

        public ColorMultiplicator multiply(ColorMultiplicator ColorMulti)
        {
            if(this.isOne)
            {
                return ColorMulti;
            }

            if(ColorMulti.isOne)
            {
                return this;
            }

            ColorMultiplicator CM = new ColorMultiplicator(0,0,0);

            CM.red = this.red + ColorMulti.red;
            CM.green = this.green + ColorMulti.green;
            CM.blue = this.blue + ColorMulti.blue;
            CM.red = clamp(CM.red, -128, 127);
            CM.green = clamp(CM.green, -128, 127);
            CM.blue = clamp(CM.blue, -128, 127);
            CM.isOne = false;

            return CM;
        }

        #endregion

    }
}
