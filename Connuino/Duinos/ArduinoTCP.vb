Imports System.Net
Imports System.Net.Sockets
Imports System.Text

Public Class ArduinoTCP
    Implements IArduino

    Public Property Text As String Implements IArduino.Text
    Public Property Name As String Implements IArduino.Name
    Public Property StatusConnect As Boolean Implements IArduino.StatusConnect

    Public Event TryConnect(sender As IArduino, e As ArduinoEvents) Implements IArduino.TryConnect
    Public Event BeforeConnect(sender As IArduino, e As ArduinoEvents) Implements IArduino.BeforeConnect
    Public Event AfterConnect(sender As IArduino, e As ArduinoEvents) Implements IArduino.AfterConnect
    Public Event Wait(sender As IArduino, e As ArduinoEvents) Implements IArduino.Wait
    Public Event Fail(sender As IArduino, e As ArduinoEvents) Implements IArduino.Fail


    Public Property IP As String
    Public Property Port As Integer


    Public Sub Init(_value As String, _text As String) Implements IArduino.Init
        Name = _value
        Text = _text
        StatusConnect = False
    End Sub

    Private _tcpClient As TcpClient
    Property TcpClient As TcpClient
        Get
            If IsNothing(_tcpClient) Then
                _networkStream = Nothing
                _tcpClient = New TcpClient()
                _tcpClient.ReceiveBufferSize = 64
                Return _tcpClient
            Else
                Return _tcpClient
            End If
        End Get
        Set(value As TcpClient)
            _tcpClient = value
        End Set
    End Property

    Private _networkStream As NetworkStream

    Property networkStream As NetworkStream
        Get
            If IsNothing(_networkStream) Then
                _networkStream = TcpClient.GetStream()
                _networkStream.ReadTimeout = 1000
                _networkStream.WriteTimeout = 1000
                Return _networkStream
            Else
                Return _networkStream
            End If
        End Get
        Set(value As NetworkStream)
            _networkStream = value
        End Set
    End Property

    Public Shared Property CallBackSuccess As Boolean = False

    Private Sub Connect_Callback(ar As IAsyncResult)
    End Sub

    Public Sub Connect(ByVal _isConnected As Boolean) Implements IArduino.Connect

        Dim _e As New ArduinoEvents
        RaiseEvent BeforeConnect(Me, _e)
        If _isConnected Then
            Reset()
            Try
                _e.Loading = True
                RaiseEvent Wait(Me, _e)
                TcpClient.BeginConnect(IP, Port, New AsyncCallback(AddressOf Connect_Callback), TcpClient)
                System.Threading.Thread.Sleep(100)
                Write("I=1;")
                System.Threading.Thread.Sleep(50)
                Write("E")
                Dim _s As String
                Dim i As Integer = 0
                While (String.IsNullOrEmpty(_s))
                    If i > 100 Then Exit While
                    _s = CStr(Read()).Trim()
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
                Dim _ipc As String = InputBox("Por favor, digite o ip e a porta", "IP:PORT", IP & ":" & Port)
                Dim _ipcs() As String = _ipc.Split(":")
                If _ipcs.Length <> 2 Then
                    _e.Exception = ex
                    RaiseEvent Fail(Me, _e)
                Else
                    IP = _ipcs(0)
                    Port = If(Integer.TryParse(_ipcs(1), True), Integer.Parse(_ipcs(1)), 80)
                    Connect(_isConnected)
                End If
            End Try

            _e.Loading = False
            RaiseEvent Wait(Me, _e)

        Else
            Reset()
            Me.StatusConnect = False
            ArduinoTCP.CallBackSuccess = False
            RaiseEvent TryConnect(Me, _e)
        End If

        RaiseEvent AfterConnect(Me, _e)
        System.Threading.Thread.Sleep(40)
    End Sub

    Public Function Write(ByVal _w As String) As Boolean Implements IArduino.Write
        If IsOpen AndAlso networkStream.CanWrite Then
            Dim sendBytes As [Byte]() = Encoding.ASCII.GetBytes(_w)
            Try
                networkStream.Write(sendBytes, 0, sendBytes.Length)
                Console.WriteLine(_w)
                Return True
            Catch ex As Exception
                Dim _e As New ArduinoEvents
                _e.Exception = ex
                RaiseEvent Fail(Me, _e)
                Return False
            End Try
        End If
    End Function

    Public Sub Reset() Implements IArduino.Reset
        TcpClient.Close()
        TcpClient = Nothing
        networkStream = Nothing
        StatusConnect = False
    End Sub


    Public Function Read() As String Implements IArduino.Read
        Try
            If IsOpen AndAlso networkStream.CanRead Then
                Dim StrValor As String = ""
                If networkStream.CanRead Then
                    If networkStream.DataAvailable Then
                        Dim bytes(16) As Byte
                        Dim i As Integer = networkStream.Read(bytes, 0, bytes.Length)
                        StrValor = Encoding.ASCII.GetString(bytes, 0, i)
                    End If
                    Return StrValor
                End If

                Return StrValor
            Else
                Return "Conecte Primeiro"
            End If
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    ReadOnly Property IsOpen As Boolean Implements IArduino.IsOpen
        Get
            Return TcpClient.Connected
        End Get
    End Property

    ReadOnly Property TextStatus As String Implements IArduino.TextStatus
        Get
            Return IP & ":" & Port
        End Get
    End Property

End Class
