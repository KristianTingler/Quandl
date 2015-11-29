Public Class DateToStringConverter
    Implements IValueConverter

    Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
        If (targetType = GetType(String)) Then
            Dim dateT As Date = DirectCast(value, Date)

            If (dateT = Date.MinValue) Then
                Return (String.Empty)
            End If

            Return (dateT.ToString("yyyy-MM-dd"))
        End If

        Return (value.ToString())
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
        If ((targetType = GetType(Date)) AndAlso (value IsNot Nothing)) Then
            If ((TypeOf value Is String) AndAlso (Not String.IsNullOrEmpty(value))) Then
                Return (Date.Parse(value.ToString()))
            End If
        End If

        Return (value)
    End Function

End Class