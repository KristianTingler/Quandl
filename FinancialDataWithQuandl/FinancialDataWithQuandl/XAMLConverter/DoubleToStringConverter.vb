Imports System.Globalization

Public Class DoubleToStringConverter
    Implements IValueConverter

    Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
        If ((targetType = GetType(String)) AndAlso (value IsNot Nothing) AndAlso (TypeOf value Is Double)) Then
            Dim val As String = DirectCast(value, Double)

            If (Double.IsNaN(val)) Then
                Return (String.Empty)
            End If
        End If

        Return (String.Format("{0:n}", value))
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
        If ((targetType = GetType(Double)) AndAlso (value IsNot Nothing)) Then
            Dim val As Double = 0

            If (Double.TryParse(value.ToString(), NumberStyles.Currency, Nothing, val)) Then
                Return (val)
            End If
        End If

        Return (value)
    End Function

End Class