using System;
using System.Globalization;
using System.IO;
using System.Net;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;

namespace MyAvalonia.Converters;

public class ImageConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null) return null;
        WebClient wc = new WebClient();
        byte[] originalData = wc.DownloadData(value.ToString() ?? string.Empty);

        MemoryStream stream = new MemoryStream(originalData);
        Bitmap bitmap = new Bitmap(stream);

        return bitmap;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}