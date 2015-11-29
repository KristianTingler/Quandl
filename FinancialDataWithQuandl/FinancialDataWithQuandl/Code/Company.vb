Imports System.ComponentModel

Public Class Company
    Implements INotifyPropertyChanged

    <DebuggerBrowsable(Diagnostics.DebuggerBrowsableState.Never)>
    Private _Name As String = String.Empty
    Public ReadOnly Property Name As String
        Get
            Return (Me._Name)
        End Get
    End Property

    <DebuggerBrowsable(Diagnostics.DebuggerBrowsableState.Never)>
    Private _Symbol As String = String.Empty
    Public ReadOnly Property Symbol As String
        Get
            Return (Me._Symbol)
        End Get
    End Property

    <DebuggerBrowsable(Diagnostics.DebuggerBrowsableState.Never)>
    Private _IsSelected As Boolean = False
    Public Property IsSelected As Boolean
        Get
            Return (Me._IsSelected)
        End Get
        Set(value As Boolean)
            Me._IsSelected = value
            Me.PropChanged("IsSelected")
        End Set
    End Property
    Public Sub New(name As String, symbol As String)
        Me._Name = name
        Me._Symbol = symbol
    End Sub

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Private Sub PropChanged(propName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propName))
    End Sub

End Class