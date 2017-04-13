Public Interface IArduino

    Property Name As String
    Property Text As String
    Property StatusConnect As Boolean
    ReadOnly Property TextStatus As String

    Event TryConnect(sender As IArduino, e As ArduinoEvents)
    Event BeforeConnect(sender As IArduino, e As ArduinoEvents)
    Event AfterConnect(sender As IArduino, e As ArduinoEvents)
    Event Wait(sender As IArduino, e As ArduinoEvents)
    Event Fail(sender As IArduino, e As ArduinoEvents)

    Sub Connect(ByVal _isConnected As Boolean)
    Sub Reset()
    Sub Init(_value As String, _text As String)
    Function Write(ByVal _w As String) As Boolean
    Function Read() As String
    ReadOnly Property IsOpen As Boolean
End Interface
