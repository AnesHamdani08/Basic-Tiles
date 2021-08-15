Public Class Utils
    Public Class CoverItem
        Public Property Type As TagLib.PictureType
        Public Property Cover As System.Drawing.Image
        Public Sub New(_Cover As System.Drawing.Image, _Type As TagLib.PictureType)
            Cover = _Cover
            Type = _Type
        End Sub
    End Class
    Public Class PeakItem
        Public Property Master As Single
        Public Property Left As Single
        Public Property Right As Single
        Public Sub New(_Master As Single, _Left As Single, _Right As Single)
            Master = _Master
            Left = _Left
            Right = _Right
        End Sub
    End Class
    Public Class PluginItem
        Public Class Entry
            Public Property Name As String
            Public Property ID As Integer
            Public Sub New(_ID As Integer, _Name As String)
                Name = _Name
                ID = _ID
            End Sub
            Public Shadows Function ToString(Spacer As String) As String
                Return ID & Spacer & Name
            End Function
        End Class
        Public Property Name As String
        Public Property ID As Integer
        Public Property Source As String
        Public Property Entries As New List(Of Entry)
        Public Sub New(_Name As String, _Src As String)
            Name = _Name
            Source = _Src
        End Sub
        Public Shadows Function ToString(Spacer As String) As String
            Dim Sb As New Text.StringBuilder
            For Each entry In Entries
                Sb.Append(Spacer & entry.ID & "|" & entry.Name)
            Next
            Return ID & Spacer & Name & Spacer & Source & Sb.ToString
        End Function
        Public Shared Function FromString(Spacer As String, StrPluginItem As String) As PluginItem
            Dim info = StrPluginItem.Split(Spacer)
            If info.Count > 3 Then
                Dim ID As Integer = info(0)
                Dim Name As String = info(1)
                Dim Source As String = info(2)
                RemoveAt(info, 0)
                RemoveAt(info, 0)
                RemoveAt(info, 0)
                Dim Entries As New List(Of Entry)
                For Each entry In info
                    Dim _info = entry.Split("|")
                    Entries.Add(New Entry(_info(0), _info(1)))
                Next
                Dim PlugItem As New PluginItem(Name, Source) With {.ID = ID, .Entries = Entries}
                Return PlugItem
            Else
                Return Nothing
            End If
        End Function
    End Class
    Public Class ComboColorItem
        Public Property Color As System.Windows.Media.Color
        Public Property InverseColor As System.Windows.Media.Color
    End Class
    Public Class GDIComboColorItem
        Public Property Color As System.Drawing.Color
        Public Property InverseColor As System.Drawing.Color
    End Class
    Public Shared ReadOnly Property FileFilters As String = "*.mp3|*.m4a|*.mp4|*.wav|*.aiff|*.mp2|*.mp1|*.ogg|*.wma|*.flac|*.alac|*.webm|*.midi|*.mid"
    Public Shared ReadOnly Property OFDFileFilters As String = "Supported Files|*.mp3;*.m4a;*.mp4;*.wav;*.aiff;*.mp2;*.mp1;*.mid;*.midi;*.ogg;*.wma;*.flac;*.alac;*.webm"
    Public Shared ReadOnly Property ImageFilters As String = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG;*.JPEG)|*.BMP;*.JPG;*.GIF;*.PNG;*.JPEG|All Files(*.*)|*.*"
    Public Shared ReadOnly Property AppDataPath As String = IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AUDX")
    Public Shared ReadOnly Property AppPath As String = IO.Path.Combine(My.Application.Info.DirectoryPath, My.Application.Info.AssemblyName & ".exe")
    Public Shared ReadOnly Property Changelog As String() = {"V #.#.#.#", " - Major : 0", " - Minor :  0", " - Minor Revision :  0", " - Build :  0", "Addition of safe listening notifier", "New Integration API Engine"}
    Public Shared ReadOnly Property V0100Changelog As String() = {"V 0.1.0.0", "-Major : 0", "- Minor :  1", "- Minor Revision : 0", "- Build  0", "- Compatibility : WIN10 10240+", "- Available Compatibility  WIN10 10240+ ; WIN7 ; WIN8", "- Changelog", "- All New visualisation engine, stutters are now gone.", "- Fixed fade audio feature", "- Fancy background has been changed", "- Folder browsing mode can now be changed", "- Autoplaying can now be turned off", "- DragDrop with changeable actions", "- Added a dedicated console window with command injecting"}
    Public Shared ReadOnly Property V0101Changelog As String() = {"V 0.1.0.1", "- Major : 0", " - Minor :  1", " - Minor Revision :  0", " - Build :  1", " - New Logo < br > ", " - Bug fixes", " - Windows explorer Open With Is now functional", " - Sleep timer Is now available", " - New Changelog popup will appear after updating", " - Launch of AUDXAPI which will let developers integrate their apps with AUDX", " - More EQ presets", " - File > Synced > Scan Is Now working", " - Settings > Scan Is Now removing song that are no longer exists", " - All New theme engine, now supports:", "  - Light", "  - Dark", "  - System Theme(Theme And Accent)", " - Custom Accent"}
    Public Shared ReadOnly Property V0200Changelog As String() = {"V 0.2.0.0", " - Major : 0", " - Minor :  2", " - Minor Revision :  0", " - Build :  0", " - Addition of a standalone tags manager", " - Fullscreen player", "  - With Background", "  - Without Background", " - Bug fixes", " - Tag Manager bug fixes:", "  - Endless cover search popup", "  - Must disconnect specified child from current parent Visual before attaching To New parent Visual.", "  - Move from;to didn't work with 2 covers", "- Live Lyrics", "- New Theme Engine ", "- Double Output", "- Single/Multi Instance(s)", "- Added Default Resume action In settings", "- YoutubeExplode Is no longer supported", "- Added support For Youtube-DL", "- Fixed Playlist Manager And Builder titlebar", "- All dependcies are Up To Date", "- Added the ability To change Tab Selector type", "- Added the ability To shuffle current playlist", "- Midi Is now supported", "- Added the ability To use custom Midi soundfonts", "- Channel rotate (""8D Audio"") should now sync With song changes", "- A-B Loop should now indicate the state In the plugins section", "- Added disable button To A-B Loop", "- Added Library To titlebar menu", "- No Updates For AUDX are scheduled till 15/07/2021}"}
    Public Shared ReadOnly Property V0210Changelog As String() = {"V 0.2.1.0", " - Major : 0", " - Minor :  2", " - Minor Revision :  1", " - Build :  0", "Libray Manual Override is now functional", "Taskbar Progress Color and Overlay Icon should now work", "DiscordRPC is no longer showing ""System.Windows.Controls.Label""", "Non-Music Youtube videos shouldn't now throw an error", "Media Next/Previous buttons should now override repeat settings", "Implemented a new notification queuing ""Notification Bucket""", "Previously executed console commands can now be scrolled through using keyboard arrow keys", "Fixed library manager foreground colors", "Fancy background settings can now be changed via settings", "Added USB Flash Drive button to titlebar for quick access to UFD operations", "Added view controls to the playlist", "Current playing track is now pinned in the top of the playlist", "Redisgned the Mini Player", "Universal Simulated Volume Hotkeys", "Repeate and Shuffle types are now saved", "Try the number guessing game in the developer console!", "Discord RPC Is now showing the app's icon instead of the full logo", "Changed the home screen layout. Now showing most played songs and random selection", "Improved loading time", "Custom playlist manager now have a refresh button", "Media controls can be docked to Top/Bottom from console", "Added Debug Logging and it's automatically enabled. You can disable it from console", "Added the ability to select a closing action:Close,Minimize To Tray or Ask Everytime", "Added a new player skin : Floating Player", "Fixed some color bugs", "Added full song info to home screen", "Player skins are now in the title bar", "All previous changelogs can now be browsed from the app , so you can see how this app has evolved over time ;)", "The loaded library will now update whenever you make changes to the library", "Tray Notification context menu icons color should now sync with the app's theme"}
    Public Shared ReadOnly Property OldChangelogs As New List(Of String())({V0210Changelog, V0200Changelog, V0101Changelog, V0100Changelog})

    <Runtime.InteropServices.DllImport("gdi32.dll", EntryPoint:="DeleteObject")>
    Public Shared Function DeleteObject(
<Runtime.InteropServices.[In]> ByVal hObject As IntPtr) As Boolean
    End Function
    Public Shared Function ImageSourceFromBitmap(ByVal bmp As System.Drawing.Bitmap, Optional ChangeRes As Boolean = False, Optional ResX As Integer = 0, Optional ResY As Integer = 0) As ImageSource
        If ChangeRes = False Then
            Try
                Dim handle = bmp.GetHbitmap()
                Try
                    Return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions())
                Finally
                    DeleteObject(handle)
                End Try
            Catch ex As Exception
                Return Nothing
            End Try
        Else
            Try
                Dim ResBmp As New System.Drawing.Bitmap(bmp, ResX, ResY)
                Dim handle = ResBmp.GetHbitmap()
                Try
                    Return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions())
                Finally
                    DeleteObject(handle)
                End Try
            Catch ex As Exception
                Return Nothing
            End Try
        End If
    End Function
    Public Shared Function BitmapFromImageSource(ByVal bitmap As BitmapSource) As System.Drawing.Bitmap
        Dim bmp As System.Drawing.Bitmap = New System.Drawing.Bitmap(bitmap.PixelWidth, bitmap.PixelHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb)
        Dim data As System.Drawing.Imaging.BitmapData = bmp.LockBits(New System.Drawing.Rectangle(System.Drawing.Point.Empty, bmp.Size), System.Drawing.Imaging.ImageLockMode.[WriteOnly], System.Drawing.Imaging.PixelFormat.Format32bppPArgb)
        bitmap.CopyPixels(Int32Rect.Empty, data.Scan0, data.Height * data.Stride, data.Stride)
        bmp.UnlockBits(data)
        Return bmp
    End Function
    Public Shared Function GetAverageColor(ByVal bitmap As BitmapSource, Optional Opacity As Integer = 255) As System.Windows.Media.Color
        If bitmap Is Nothing Then Return Colors.Black
        Dim format = bitmap.Format
        If format <> PixelFormats.Bgr24 AndAlso format <> PixelFormats.Bgr32 AndAlso format <> PixelFormats.Bgra32 AndAlso format <> PixelFormats.Pbgra32 Then
            Throw New InvalidOperationException("BitmapSource must have Bgr24, Bgr32, Bgra32 Or Pbgra32 format")
            Return Nothing
        End If

        Dim width = bitmap.PixelWidth
        Dim height = bitmap.PixelHeight
        Dim numPixels = width * height
        Dim bytesPerPixel = format.BitsPerPixel / 8
        Dim pixelBuffer = New Byte(numPixels * bytesPerPixel - 1) {}
        bitmap.CopyPixels(pixelBuffer, width * bytesPerPixel, 0)
        Dim blue As Long = 0
        Dim green As Long = 0
        Dim red As Long = 0
        Dim i As Integer = 0

        While i < pixelBuffer.Length
            blue += pixelBuffer(i)
            green += pixelBuffer(i + 1)
            red += pixelBuffer(i + 2)
            i += bytesPerPixel
        End While

        Return System.Windows.Media.Color.FromArgb(CByte(Opacity), CByte((red / numPixels)), CByte((green / numPixels)), CByte((blue / numPixels)))
    End Function
    Public Shared Function GetInverseColor(Color As System.Windows.Media.Color, Optional Opacity As Integer = 255) As System.Windows.Media.Color
        Dim R = Color.R
        Dim G = Color.G
        Dim B = Color.B
        Dim newR = 255 - R
        Dim newG = 255 - G
        Dim newB = 255 - B
        Return System.Windows.Media.Color.FromArgb(Opacity, newR, newG, newB)
    End Function
    Public Shared Function GetInverseColor(Color As System.Drawing.Color, Optional Opacity As Integer = 255) As System.Drawing.Color
        Dim R = Color.R
        Dim G = Color.G
        Dim B = Color.B
        Dim newR = 255 - R
        Dim newG = 255 - G
        Dim newB = 255 - B
        Return System.Drawing.Color.FromArgb(Opacity, newR, newG, newB)
    End Function
    Public Shared Function SecsToMins(sec As Double, Optional LongText As Boolean = False) As String
        Try
            If LongText = False Then
                Dim Pos = sec / 60
                Dim Mins As String
                Try
                    Mins = Pos.ToString.Substring(0, Pos.ToString.IndexOf("."))
                Catch ex As Exception
                    Mins = Pos.ToString
                End Try
                Dim secs As String
                Try
                    secs = Pos.ToString.Substring(Pos.ToString.IndexOf("."), Pos.ToString.Length - Pos.ToString.IndexOf("."))
                Catch ex As Exception
                    secs = 0
                End Try
                Dim ConvSecs = secs * 60
                If Mins >= 10 AndAlso Math.Round(ConvSecs) >= 10 Then
                    Return New String(Mins & ":      " & Math.Round(ConvSecs))
                ElseIf Mins < 10 AndAlso Math.Round(ConvSecs) >= 10 Then
                    Return New String("0" & Mins & ":" & Math.Round(ConvSecs))
                ElseIf Mins >= 10 AndAlso Math.Round(ConvSecs) < 10 Then
                    Return New String(Mins & ":" & "0" & Math.Round(ConvSecs))
                ElseIf Mins < 10 Or Mins = 0 AndAlso Math.Round(ConvSecs) < 10 Then
                    Return New String("0" & Mins & ":" & "0" & Math.Round(ConvSecs))
                ElseIf Mins < 10 AndAlso Math.Round(ConvSecs) > 59 Then
                    Return New String("0" & Mins & ":" & "00")
                ElseIf Mins > 10 AndAlso Math.Round(ConvSecs) > 59 Then
                    Return New String(Mins & ":" & "00")
                End If
            ElseIf LongText = True Then
                Dim DeciMins = sec / 60
                Dim IntMins = DeciMins.ToString.Substring(0, DeciMins.ToString.IndexOf("."))
                Dim DeciSecs = DeciMins.ToString.Substring(DeciMins.ToString.IndexOf("."), DeciMins.ToString.Length - DeciMins.ToString.IndexOf("."))
                Dim IntSecs = DeciSecs * 60
                If IntSecs < 10 Then
                    Return New String(IntMins & " min 0" & Math.Round(IntSecs) & " sec")
                Else
                    Return New String(IntMins & " min " & Math.Round(IntSecs) & " sec")
                End If
            End If
        Catch ex As Exception
            Return "ERROR"
        End Try
    End Function
    Public Shared Function GetMins(sec As Double)
        Dim mins = sec / 60
        Try
            Return mins.ToString.Substring(0, mins.ToString.IndexOf("."))
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Public Shared Function GetRestSecs(sec As Double)
        Dim mins = sec / 60
        Dim secs = mins.ToString.Substring(mins.ToString.IndexOf(".")) * 60
        Try
            Return secs
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Public Shared Function SecsToMs(sec As Double) As TimeSpan
        Dim Pos = sec.ToString.IndexOf(".")
        Dim Secs As Integer
        Dim Ms As Integer
        If Pos = -1 Then
            Secs = 0
            Ms = 0
        Else
            Secs = sec.ToString.Substring(0, Pos)
            Ms = sec.ToString.Substring(Pos) * 1000
        End If
        Ms += (Secs * 1000)
        Return TimeSpan.FromMilliseconds(Ms)
    End Function
    ''' <summary>
    ''' Artist, Title, Album, Year, TrackNum, Genres, Lyrics, Bitrates,Source
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    Public Shared Function GetSongInfo(path As String) As String()
        Try
            Dim Tag = TagLib.File.Create(path)
            Dim Artist = Tag.Tag.JoinedPerformers
            Dim Title = Tag.Tag.Title
            Dim Album = Tag.Tag.Album
            Dim Year = Tag.Tag.Year
            Dim TrackNum = Tag.Tag.TrackCount
            Dim Lyrics = Tag.Tag.Lyrics
            Dim Genres = Tag.Tag.Genres
            Dim Bitrates = Tag.Properties.AudioBitrate
            Dim CompressedInfo As String()
            CompressedInfo = {Artist, Title, Album, Year, TrackNum, String.Join(";", Genres), Lyrics, Bitrates, path}
            Return CompressedInfo
        Catch ex As Exception
            'Return {"Not Available", "Not Available", "Not Available", 0, 0, "Not Available", "Not Available", 0}
            Return {Nothing, Nothing, Nothing, 0, 0, Nothing, Nothing, 0, path}
        End Try
    End Function
    Public Shared Function GetAlbumArt(path As String) As System.Drawing.Image
        Try
            Dim Tag = TagLib.File.Create(path)
            If Tag.Tag.Pictures.Length >= 1 Then
                Dim bin = Tag.Tag.Pictures(0).Data.Data
                Dim Cover = System.Drawing.Image.FromStream(New IO.MemoryStream(bin))
                Return Cover
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function GetAlbumArts(path As String) As List(Of CoverItem)
        Dim Arts As New List(Of CoverItem)
        Try
            Dim Tag = TagLib.File.Create(path)
            If Tag.Tag.Pictures.Length >= 1 Then
                For Each art In Tag.Tag.Pictures
                    Dim bin = DirectCast(art.Data.Data, Byte())
                    Arts.Add(New CoverItem(System.Drawing.Image.FromStream(New IO.MemoryStream(bin)), art.Type))
                    bin = Nothing
                Next
                Return Arts
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Sub SaveToPng(ByVal visual As ImageSource, ByVal fileName As String)
        Dim encoder = New PngBitmapEncoder()
        SaveUsingEncoder(visual, fileName, encoder)
    End Sub
    Private Shared Sub SaveUsingEncoder(ByVal source As ImageSource, ByVal fileName As String, ByVal encoder As BitmapEncoder)
        Dim frame As BitmapFrame = BitmapFrame.Create(source)
        encoder.Frames.Add(frame)
        Using stream = IO.File.Create(fileName)
            encoder.Save(stream)
        End Using
    End Sub
    Public Shared Function Shuffle(list As String()) As String()
        Dim Rnd As New Random()
        Dim j As Int32
        Dim temp As String
        Dim items As String() = list
        For n As Int32 = items.Length - 1 To 0 Step -1
            j = Rnd.Next(0, n + 1)
            ' Swap them.
            temp = items(n)
            items(n) = items(j)
            items(j) = temp
        Next n
        Return items
    End Function
    Public Shared Function Shuffle(list As List(Of String)) As List(Of String)
        Dim Rnd As New Random()
        Dim j As Int32
        Dim temp As String
        Dim items As List(Of String) = list
        For n As Int32 = items.Count - 1 To 0 Step -1
            j = Rnd.Next(0, n + 1)
            ' Swap them.
            temp = items(n)
            items(n) = items(j)
            items(j) = temp
        Next n
        Return items
    End Function
    Public Shared Function IntToModSTR(idx As Integer) As String
        Select Case idx
            Case 0
                Return "NOMOD"
            Case 1
                Return "SHIFT"
            Case 2
                Return "CTRL"
            Case 3
                Return "ALT"
            Case 4
                Return "WIN"
            Case Else
                Return 0
        End Select
    End Function
    Public Shared Function FileSizeConverter(Size As ULong) As Double
        Try
            Dim DoubleBytes As Double
            Select Case Size
                Case Is >= 1099511627776
                    DoubleBytes = CDbl(Size / 1099511627776) 'TB
                    Return FormatNumber(DoubleBytes, 2)
                Case 1073741824 To 1099511627775
                    DoubleBytes = CDbl(Size / 1073741824) 'GB
                    Return FormatNumber(DoubleBytes, 2)
                Case 1048576 To 1073741823
                    DoubleBytes = CDbl(Size / 1048576) 'MB
                    Return FormatNumber(DoubleBytes, 2)
                Case 1024 To 1048575
                    DoubleBytes = CDbl(Size / 1024) 'KB
                    Return FormatNumber(DoubleBytes, 2)
                Case 0 To 1023
                    DoubleBytes = Size ' bytes
                    Return FormatNumber(DoubleBytes, 2)
                Case Else
            End Select
        Catch
            Return 0
        End Try
    End Function
    Public Shared Function FileSizeConverterSTR(Size As ULong) As String
        Try
            Dim DoubleBytes As Double
            Select Case Size
                Case Is >= 1099511627776
                    DoubleBytes = CDbl(Size / 1099511627776) 'TB
                    Return FormatNumber(DoubleBytes, 2) & "TB"
                Case 1073741824 To 1099511627775
                    DoubleBytes = CDbl(Size / 1073741824) 'GB
                    Return FormatNumber(DoubleBytes, 2) & "GB"
                Case 1048576 To 1073741823
                    DoubleBytes = CDbl(Size / 1048576) 'MB
                    Return FormatNumber(DoubleBytes, 2) & "MB"
                Case 1024 To 1048575
                    DoubleBytes = CDbl(Size / 1024) 'KB
                    Return FormatNumber(DoubleBytes, 2) & "KB"
                Case 0 To 1023
                    DoubleBytes = Size ' bytes
                    Return FormatNumber(DoubleBytes, 2) & "Bytes"
                Case Else
            End Select
        Catch
            Return 0
        End Try
    End Function
    Public Shared Sub RemoveAt(Of T)(ByRef arr As T(), ByVal index As Integer)
        Dim uBound = arr.GetUpperBound(0)
        Dim lBound = arr.GetLowerBound(0)
        Dim arrLen = uBound - lBound

        If index < lBound OrElse index > uBound Then
            Throw New ArgumentOutOfRangeException(
        String.Format("Index must be from {0} to {1}.", lBound, uBound))

        Else
            'create an array 1 element less than the input array
            Dim outArr(arrLen - 1) As T
            'copy the first part of the input array
            Array.Copy(arr, 0, outArr, 0, index)
            'then copy the second part of the input array
            Array.Copy(arr, index + 1, outArr, index, uBound - index)

            arr = outArr
        End If
    End Sub
    Public Shared Function NumFromSTR(ByVal value As String) As Integer
        Dim returnVal As String = String.Empty
        Dim collection As Text.RegularExpressions.MatchCollection = Text.RegularExpressions.Regex.Matches(value, "\d+")
        For Each m As Text.RegularExpressions.Match In collection
            returnVal += m.ToString()
        Next
        Return Convert.ToInt32(returnVal)
    End Function
    Public Shared Function ValToPercentage(Val As Double, Min As Double, Max As Double) As Integer
        If Val > Min AndAlso Val < Max Then
            Return ((Val - Min) * 100) / (Max - Min)
        ElseIf Val = Max Then
            Return 100
        Else
            Return 0
        End If
    End Function
    Public Shared Function PercentageToVal(Percentage As Double, Min As Double, Max As Double) As Integer
        If Percentage > 0 AndAlso Percentage < 100 Then
            Return (((Max - Min) * Percentage) + (Min * 100)) / 100
        ElseIf Percentage >= 100 Then
            Return Max
        Else
            Return Min
        End If
    End Function
    Public Shared Function PercentageToFiveMax(Percentage As Double) As Integer
        Dim PercentageChunk As Integer = 20
        If Percentage = 0 Then
            Return 0
        ElseIf IsInRange(Percentage, 0, 20) Then
            Return 1
        ElseIf IsInRange(Percentage, 20, 40, True) Then
            Return 2
        ElseIf IsInRange(Percentage, 40, 60, True) Then
            Return 3
        ElseIf IsInRange(Percentage, 60, 80, True) Then
            Return 4
        ElseIf IsInRange(Percentage, 80, 100, True, True) Then
            Return 5
        End If
    End Function
    Public Shared Function IsInRange(Val As Integer, Min As Integer, Max As Integer, Optional IsEqualLower As Boolean = False, Optional IsEqualGreater As Boolean = False) As Boolean
        If IsEqualLower = True AndAlso IsEqualLower = False Then
            If Val >= Min AndAlso Val < Max Then
                Return True
            Else
                Return False
            End If
        ElseIf IsEqualLower = True AndAlso IsEqualLower = True Then
            If Val >= Min AndAlso Val <= Max Then
                Return True
            Else
                Return False
            End If
        ElseIf IsEqualLower = False AndAlso IsEqualLower = True Then
            If Val > Min AndAlso Val <= Max Then
                Return True
            Else
                Return False
            End If
        ElseIf IsEqualLower = False AndAlso IsEqualLower = False Then
            If Val > Min AndAlso Val < Max Then
                Return True
            Else
                Return False
            End If
        Else
            If Val > Min AndAlso Val < Max Then
                Return True
            Else
                Return False
            End If
        End If
    End Function
    Public Shared Function ReverseToMin(Val As Double, Min As Double, Max As Double) As Double
        Return (Max - Val) + Min
    End Function
    Public Shared Function ReverseToMax(Val As Double, Min As Double, Max As Double) As Double
        Return Max - (Val - Min)
    End Function
    Public Shared Function GetGDIRandomColorWithInverse() As GDIComboColorItem
        Dim RND As New Random
        Dim R = RND.Next(0, 256)
        Dim G = RND.Next(0, 256)
        Dim B = RND.Next(0, 256)
        Dim Color = System.Drawing.Color.FromArgb(R, G, B)
        Return New GDIComboColorItem With {.Color = Color, .InverseColor = GetInverseColor(Color)}
    End Function
    Public Shared Function GetRandomColorWithInverse() As ComboColorItem
        Dim RND As New Random
        Dim R = RND.Next(0, 256)
        Dim G = RND.Next(0, 256)
        Dim B = RND.Next(0, 256)
        Dim Color = System.Windows.Media.Color.FromArgb(255, R, G, B)
        Return New ComboColorItem With {.Color = Color, .InverseColor = GetInverseColor(Color)}
    End Function
#Region "Enums"
    Public Enum DragDropPlaylistBehaviour
        Replace = 0
        AddToFirst = 1
        AddToLast = 2
        AddToNext = 3
        AddToLastPlay = 4
    End Enum
    Public Enum FileSize
        TB
        GB
        MB
        KB
        B
    End Enum
#End Region
End Class