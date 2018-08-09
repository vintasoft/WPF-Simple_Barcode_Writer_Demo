using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Media;


namespace WpfSimpleBarcodeWriterDemo
{
    /// <summary> 
    /// Converts <see cref="System.Windows.Media.Color"/> objects from one data type to another.
    /// </summary>
    public class SimpleColorConverter : TypeConverter
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleColorConverter"/> class.
        /// </summary>
        public SimpleColorConverter()
        {
        }

        #endregion



        #region Methods

        /// <summary>
        /// Determines whether this converter can convert an object in the specified source
        /// type to the native type of the converter.
        /// </summary>
        /// <param name="context">A formatter context.</param>
        /// <param name="sourceType">The type you want to convert from.</param>
        /// <returns><b>true</b> if this object can perform the conversion; otherwise, <b>false</b>.</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        /// <summary>
        /// Returns whether this converter can convert the object to the specified type.
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext object that provides a format context.</param>
        /// <param name="destinationType">A Type that represents the type you want to convert to.</param>
        /// <returns><b>true</b> if this converter can perform the conversion; otherwise, <b>false</b>.</returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return true;

            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Converts the specified object to the native type of the converter.
        /// </summary>
        /// <param name="context">A formatter context.</param>
        /// <param name="culture">A CultureInfo object that specifies the culture used to represent the font.</param>
        /// <param name="value">The object to convert.</param>
        /// <returns>The converted object.</returns>
        public override object ConvertFrom(
            ITypeDescriptorContext context,
            CultureInfo culture,
            object value)
        {
            try
            {
                if (value is string)
                {
                    string strValue = value.ToString();

                    string[] splitedValue = strValue.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    if (splitedValue.Length == 1)
                    {
                        byte channel = Convert.ToByte(splitedValue[0]);

                        return Color.FromRgb(channel, channel, channel);
                    }
                    else if (splitedValue.Length == 3 || splitedValue.Length == 4)
                    {
                        byte a = 255;
                        byte r;
                        byte g;
                        byte b;

                        int startIndex = 0;
                        if (splitedValue.Length == 4)
                        {
                            a = Convert.ToByte(splitedValue[0]);
                            startIndex = 1;
                        }

                        r = Convert.ToByte(splitedValue[startIndex + 0]);
                        g = Convert.ToByte(splitedValue[startIndex + 1]);
                        b = Convert.ToByte(splitedValue[startIndex + 2]);

                        return Color.FromArgb(a, r, g, b);
                    }
                }
            }
            catch
            {
            }

            return null;
        }

        /// <summary>
        /// Converts the specified object to another type.
        /// </summary>
        /// <param name="context">A formatter context.</param>
        /// <param name="culture">A CultureInfo object that specifies the culture used to represent the object.</param>
        /// <param name="value">The object to convert.</param>
        /// <param name="destinationType">The data type to convert the object to.</param>
        /// <returns>The converted object.</returns>
        public override object ConvertTo(
            ITypeDescriptorContext context,
            CultureInfo culture,
            object value,
            Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                Color color = (Color)value;

                string result = string.Empty;
                if (color.A != 255)
                    result += string.Format("{0}; ", color.A);

                result += string.Format("{0}; {1}; {2}", color.R, color.G, color.B);
                return result;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        #endregion

    }
}
