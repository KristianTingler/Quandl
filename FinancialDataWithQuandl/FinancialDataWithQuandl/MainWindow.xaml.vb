Imports System.Data

Class MainWindow

    Public ReadOnly Property DC As ViewModel
        Get
            Return (TryCast(Me.DataContext, ViewModel))
        End Get
    End Property

    Public Sub New()
        InitializeComponent()

        Me.DataContext = ViewModel.Instance

        If (Me.DC IsNot Nothing) Then
            Me.DC.Init()
        End If
    End Sub

    Private Sub NiceButton_MouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs)
        If ((sender IsNot Nothing) AndAlso (TypeOf sender Is FrameworkElement)) Then
            Dim fe As FrameworkElement = CType(sender, FrameworkElement)

            If ((fe.DataContext IsNot Nothing) AndAlso (TypeOf fe.DataContext Is Company)) Then
                Me.DC.GetData(CType(fe.DataContext, Company))
            End If
        End If
    End Sub

End Class