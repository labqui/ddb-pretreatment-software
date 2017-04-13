Imports System.IO.Ports

Public Class ArduinoCom
    Inherits SerialPort
    Implements IArduino

    Public Property Text As String Implements IArduino.Text
    Public Property Name As String Implements IArduino.Name
    Public Property StatusConnect As Boolean Implements IArduino.StatusConnect

    Public Event TryConnect(sender As IArduino, e As ArduinoEvents) Implements IArduino.TryConnect
    Public Event BeforeConnect(sender As IArduino, e As ArduinoEvents) Implements IArduino.BeforeConnect
    Public Event AfterConnect(sender As IArduino, e As ArduinoEvents) Implements IArduino.AfterConnect
    Public Event Wait(sender As IArduino, e As ArduinoEvents) Implements IArduino.Wait
    Public Event Fail(sender As IArduino, e As ArduinoEvents) Implements IArduino.Fail

    Public Sub Init(_value As String, _text As String) Implements IArduino.Init
        PortName = _value
        Name = _value
        Text = _text
        BaudRate = 9600
        DataBits = 8
        Encoding = System.Text.Encoding.Default
        StatusConnect = False
    End Sub


    Public Sub Connect(ByVal _isConnected As Boolean) Implements IArduino.Connect
        Dim _e As New ArduinoEvents
        RaiseEvent BeforeConnect(Me, _e)
        If _isConnected Then
            Reset()
            _e.Loading = True
            RaiseEvent Wait(Me, _e)
            Try
                Open()
                System.Threading.Thread.Sleep(100)
                Write("I=1;")
                System.Threading.Thread.Sleep(50)
                Write("E")
                Dim _s As String
                Dim i As Integer = 0
                While (String.IsNullOrEmpty(_s))
                    If i > 100 Then Exit While
                    _s = Read()
                    System.Threading.Thread.Sleep(40)
                    i += 1
                End While

                If (_s = "labqui") Then
                    StatusConnect = True
                    ArduinoTCP.CallBackSuccess = True
                Else
                    StatusConnect = False
                    ArduinoTCP.CallBackSuccess = False
                    Throw New Exception("Error")
                End If
                RaiseEvent TryConnect(Me, _e)

            Catch ex As Exception
                _e.Exception = ex
                RaiseEvent Fail(Me, _e)
            End Try
            _e.Loading = False
            RaiseEvent Wait(Me, _e)
        Else
            Reset()
            Close()
            Me.StatusConnect = False
            RaiseEvent TryConnect(Me, _e)
        End If
        RaiseEvent AfterConnect(Me, _e)
        System.Threading.Thread.Sleep(40)
    End Sub

    Public Function Write(ByVal _w As String) As Boolean Implements IArduino.Write
        If IsOpen Then
            Try
                MyBase.Write(_w)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End If
        Return False
    End Function

    Public Sub Reset() Implements IArduino.Reset
        Write("R")
    End Sub


    Public Function Read() As String Implements IArduino.Read
        If IsOpen Then
            Dim StrValor As String = ""
            If BaseStream.CanRead And BytesToRead > 0 Then
                Dim bytes(16) As Byte
                Dim i As Integer = BaseStream.Read(bytes, 0, bytes.Length)
                StrValor = Encoding.ASCII.GetString(bytes, 0, i)
            End If
            Return StrValor
        Else
            Return "Conecte Primeiro"
        End If
    End Function

    ReadOnly Property IsOpen As Boolean Implements IArduino.IsOpen
        Get
            Return MyBase.IsOpen
        End Get
    End Property

    ReadOnly Property TextStatus As String Implements IArduino.TextStatus
        Get
            Return Name
        End Get
    End Property

End Class
