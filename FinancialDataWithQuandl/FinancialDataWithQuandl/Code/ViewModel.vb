Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Data
Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Text

Public Class ViewModel
    Implements INotifyPropertyChanged

    <DebuggerBrowsable(Diagnostics.DebuggerBrowsableState.Never)>
    Private Shared _Instance As ViewModel = Nothing
    Public Shared ReadOnly Property Instance As ViewModel
        Get
            If (_Instance Is Nothing) Then
                _Instance = New ViewModel()
                '_Instance.Init()
            End If

            Return (_Instance)
        End Get
    End Property

    <DebuggerBrowsable(Diagnostics.DebuggerBrowsableState.Never)>
    Private _HistoricalPrices As ObservableCollection(Of HistoricalPrice) = New ObservableCollection(Of HistoricalPrice)
    Public ReadOnly Property HistoricalPrices As ObservableCollection(Of HistoricalPrice)
        Get
            Return (Me._HistoricalPrices)
        End Get
    End Property

    <DebuggerBrowsable(Diagnostics.DebuggerBrowsableState.Never)>
    Private _Companies As ObservableCollection(Of Company) = New ObservableCollection(Of Company)
    Public ReadOnly Property Companies As ObservableCollection(Of Company)
        Get
            Return (Me._Companies)
        End Get
    End Property

    Public Sub Init()
        Me.Companies.Add(New Company("Coca Cola", "KO"))
        Me.Companies.Add(New Company("Berkshire Hathaway", "BRK_A"))
        Me.Companies.Add(New Company("Johnson & Johnson", "JNJ"))
        Me.Companies.Add(New Company("Procter & Gamble", "PG"))
        Me.Companies.Add(New Company("Moody's", "MCO"))
        Me.Companies.Add(New Company("Microsoft", "MSFT"))
        Me.Companies.Add(New Company("McDonalds", "MCD"))
        Me.Companies.Add(New Company("USG Corp", "USG"))

        Me.GetData(Me.Companies.First)
    End Sub

    Public Sub GetData(c As Company)
        For Each comp As Company In Me.Companies
            comp.IsSelected = False
        Next

        c.IsSelected = True

        Dim url As String = String.Format("https://www.quandl.com/api/v1/datasets/WIKI/{0}.xml?trim_start={1}", c.Symbol, (New Date(Date.Now.Year, Date.Now.Month - 1, 1)).ToString("yyyy-MM-dd"))
        Dim dataSet As String = Me.Download(url)

        If (Not String.IsNullOrWhiteSpace(dataSet)) Then
            Dim ds As XElement = XElement.Parse(dataSet)

            If (ds.HasElements) Then
                Dim dataX As XElement = ds.Element("data")

                If (dataX IsNot Nothing) Then
                    Me.HistoricalPrices.Clear()

                    For Each datum As XElement In dataX.Elements("datum")
                        Dim data As HistoricalPrice = New HistoricalPrice()

                        data.Date = Date.Parse(datum.Elements()(0).Value)
                        data.Open = Double.Parse(datum.Elements()(1).Value, NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-GB"))
                        data.High = Double.Parse(datum.Elements()(2).Value, NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-GB"))
                        data.Low = Double.Parse(datum.Elements()(3).Value, NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-GB"))
                        data.Close = Double.Parse(datum.Elements()(4).Value, NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-GB"))
                        data.Volume = Double.Parse(datum.Elements()(5).Value, NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-GB"))
                        data.ExDividend = Double.Parse(datum.Elements()(6).Value, NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-GB"))
                        data.SplitRatio = Double.Parse(datum.Elements()(7).Value, NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-GB"))
                        data.AdjOpen = Double.Parse(datum.Elements()(8).Value, NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-GB"))
                        data.AdjHigh = Double.Parse(datum.Elements()(9).Value, NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-GB"))
                        data.AdjLow = Double.Parse(datum.Elements()(10).Value, NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-GB"))
                        data.AdjClose = Double.Parse(datum.Elements()(11).Value, NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-GB"))
                        data.AdjVolume = Double.Parse(datum.Elements()(12).Value, NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-GB"))

                        Me.HistoricalPrices.Add(data)
                    Next
                End If
            End If
        End If
    End Sub

    Private Function Download(url As String) As String
        Dim strR As String = Nothing

        Try
            Dim client As WebClient = New WebClient()
            client.Headers.Add("Accept", "text/html, Application / xhtml + Xml, */* ")
            client.Headers.Add("Accept-Language", "de-DE")

            Dim data As Stream = client.OpenRead(url)
            Dim reader As StreamReader = New StreamReader(data)
            Dim downloadedStr As StringBuilder = New StringBuilder()

            Dim str As String = String.Empty
            str = reader.ReadLine()

            If (str IsNot Nothing) Then
                Do While (str IsNot Nothing)
                    str = reader.ReadLine()
                    downloadedStr.Append(str)
                Loop
            End If

            strR = downloadedStr.ToString()
        Catch ex As Exception
            strR = Nothing
        End Try

        Return (strR)
    End Function

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Private Sub PropChanged(propName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propName))
    End Sub

End Class