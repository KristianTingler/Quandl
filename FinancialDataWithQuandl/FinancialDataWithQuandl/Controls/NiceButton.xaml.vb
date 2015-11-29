Public Class NiceButton

    Public Shared TextProperty As DependencyProperty = DependencyProperty.Register("Text", GetType(String), GetType(NiceButton), New PropertyMetadata(AddressOf _OnTextChanged))

    Private Shared Sub _OnTextChanged(ByVal sender As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
        If ((sender IsNot Nothing) AndAlso (TypeOf sender Is NiceButton)) Then
            If ((TypeOf sender Is NiceButton) AndAlso (e.NewValue IsNot Nothing)) Then
                Dim mySelf As NiceButton = DirectCast(sender, NiceButton)
                mySelf.txtText.Text = e.NewValue
            End If
        End If
    End Sub

    Public Property Text As String
        Get
            Return (CType(GetValue(TextProperty), String))
        End Get
        Set(value As String)
            SetValue(TextProperty, value)
        End Set
    End Property

    Public Shared Shadows BackgroundProperty As DependencyProperty = DependencyProperty.Register("Background", GetType(String), GetType(NiceButton), New PropertyMetadata(AddressOf _OnBackgroundChanged))

    Private Shared Sub _OnBackgroundChanged(ByVal sender As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
        If ((sender IsNot Nothing) AndAlso (TypeOf sender Is NiceButton)) Then
            If ((TypeOf sender Is NiceButton) AndAlso (e.NewValue IsNot Nothing)) Then
                Dim mySelf As NiceButton = DirectCast(sender, NiceButton)
                Dim bc As BrushConverter = New BrushConverter()
                Dim br As Brush = bc.ConvertFromString(e.NewValue)

                mySelf.BackgroundBorder.Background = br
            End If
        End If
    End Sub

    Public Overloads Property Background As String
        Get
            Return (CType(GetValue(BackgroundProperty), String))
        End Get
        Set(value As String)
            SetValue(BackgroundProperty, value)
        End Set
    End Property

    Public Shared IsSelectedProperty As DependencyProperty = DependencyProperty.Register("IsSelected", GetType(Boolean), GetType(NiceButton), New PropertyMetadata(AddressOf _OnIsSelectedChanged))

    Private Shared Sub _OnIsSelectedChanged(ByVal sender As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
        If ((sender IsNot Nothing) AndAlso (TypeOf sender Is NiceButton)) Then
            If ((TypeOf sender Is NiceButton) AndAlso (e.NewValue IsNot Nothing)) Then
                Dim mySelf As NiceButton = DirectCast(sender, NiceButton)

                mySelf.selectedBorder.Visibility = Visibility.Collapsed

                If (e.NewValue) Then
                    mySelf.selectedBorder.Visibility = Visibility.Visible
                End If
            End If
        End If
    End Sub

    Public Property IsSelected As Boolean
        Get
            Return (CType(GetValue(IsSelectedProperty), Boolean))
        End Get
        Set(value As Boolean)
            SetValue(IsSelectedProperty, value)
        End Set
    End Property

    Private Sub Button_MouseEnter(sender As System.Object, e As System.Windows.Input.MouseEventArgs)
        Me.hoverBorder.Visibility = Visibility.Visible
    End Sub

    Private Sub Button_MouseLeave(sender As System.Object, e As System.Windows.Input.MouseEventArgs)
        Me.hoverBorder.Visibility = Visibility.Collapsed
    End Sub

End Class