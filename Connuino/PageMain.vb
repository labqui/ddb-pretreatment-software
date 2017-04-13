Imports System.Management
Imports System.Text.RegularExpressions

Public Class PageMain

    Public ReadOnly Property Arduino As IArduino
        Get
            Dim _p As IArduino = CType(ComboPortas.SelectedItem, IArduino)
            AddHandler _p.Wait, AddressOf LoadingConnection
            AddHandler _p.TryConnect, AddressOf isConnected
            AddHandler _p.Fail, AddressOf MsgFail
            Return _p
        End Get
    End Property

    Private Sub isConnected(sender As IArduino, e As ArduinoEvents)
        If sender.StatusConnect Then
            TimerRead.Enabled = True
            ComboPortas.Enabled = False
            Text = String.Format("Conectado a {0}", sender.TextStatus)
        Else
            TimerRead.Enabled = False
            'CheckBox2.CheckState = False
            ComboPortas.Enabled = True
            Text = String.Format("Desconectado a {0}", sender.TextStatus)
        End If
    End Sub

    Public Property _port As Integer = 80

    Public Property Port As Integer
        Get
            Return _port
        End Get
        Set(value As Integer)
            _port = value
        End Set
    End Property

    Public Property _ip As String = "192.168.1.150"

    Public Property IP As String
        Get
            Return _ip
        End Get
        Set(value As String)
            _ip = value
        End Set
    End Property

    Private Sub MsgFail(sender As IArduino, e As ArduinoEvents)
        MsgBox(e.Exception.Message, MsgBoxStyle.Critical, "Um erro ocorreu")
    End Sub

    Public Sub LoadingConnection(sender As IArduino, e As ArduinoEvents)
        If e.Loading Then
            ComboPortas.Enabled = False
            Me.Cursor = Cursors.WaitCursor
        Else
            Me.Cursor = Cursors.Default
        End If
    End Sub


    Friend Shared Sub StatusSend(v As String)
        Console.WriteLine(v)
    End Sub


    Public Sub Form1_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboPortas.ValueMember = "Name"
        ComboPortas.DisplayMember = "Text"
        Try
            Dim searcher As New ManagementObjectSearcher("root\cimv2", "SELECT * FROM Win32_SerialPort")
            Dim _rgx As New Regex("((A|a)(rduino))|CP[0-9]{1}", RegexOptions.IgnorePatternWhitespace)
            For Each queryObj As ManagementObject In searcher.Get()
                If _rgx.IsMatch(queryObj("Name")) Then
                    Dim _p As New ArduinoCom()
                    _p.Init(queryObj("DeviceID"), queryObj("Name"))
                    ComboPortas.Items.Add(_p)
                End If
            Next

            Dim _e As New ArduinoTCP()
            _e.IP = IP
            _e.Port = Port
            _e.Init("Ethernet", "Ethernet")
            ComboPortas.Items.Add(_e)

            ComboPortas.SelectedIndex = ComboPortas.Items.Count - 1
        Catch err As ManagementException
            MsgBox(err.Message)
        End Try
    End Sub



    Public Sub Button1_Click(sender As Object, e As System.EventArgs) Handles abortar.Click
        Arduino.Write("E")
    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles TimerRead.Tick
        Dim _r As String = Arduino.Read()
        If _r <> "" Then StatusField.Text = _r
    End Sub

    Private Sub TrackBar1_Scroll(sender As TrackBar, e As EventArgs) Handles TrackBar1.ValueChanged
        Arduino.Write("B=" & sender.Value & ";")
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Arduino.Write("A=;")
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As CheckBox, e As EventArgs) Handles CheckBox1.CheckedChanged
        Arduino.Write("D=" & If(sender.Checked, "1", "0") & ";")
        Arduino.Write("E")
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        Arduino.Connect(sender.Checked)
    End Sub

    Private Sub TrackBar1_Leave(sender As Object, e As EventArgs) Handles TrackBar1.Leave
        Arduino.Write("A=;")
        Arduino.Write("E")
    End Sub

End Class