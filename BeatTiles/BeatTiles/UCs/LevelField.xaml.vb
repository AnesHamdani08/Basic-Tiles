Imports BeatTiles

Public Class LevelField
    Private Property VisColor1 As System.Drawing.Color = System.Drawing.Color.Black
    Private Property VisColor2 As System.Drawing.Color = System.Drawing.Color.Red
    Public WithEvents LvGen As New LevelGenerator
    Public Sub StartLevel()
        LvGen.Lanes = {G1, G2, G3, G4}
        'Dim OFD As New Forms.OpenFileDialog With {.CheckFileExists = True, .Filter = Utils.OFDFileFilters}
        'If OFD.ShowDialog Then
        LvGen.SongFile = "Song Here" 'OFD.FileName
        Dim VisUpdater As New Forms.Timer With {.Interval = 16}
        AddHandler VisUpdater.Tick, Sub()
                                        Visuo.Source = Utils.ImageSourceFromBitmap(LvGen.Player.CreateVisualizer(Player.Visualizers.SpectumLine, LevelField.ActualWidth, LevelField.ActualHeight, VisColor1, VisColor2, System.Drawing.Color.Empty, System.Drawing.Color.Empty, 10, 2, 2, 100, False, False, False))
                                    End Sub
        AddHandler LvGen.Player.MediaLoaded, Async Sub(Title As String, Artist As String, Cover As System.Windows.Interop.InteropBitmap, Thumb As System.Windows.Interop.InteropBitmap, LyricsAvailable As Boolean, Lyrics As String)
                                                 If Cover IsNot Nothing Then
                                                     LevelField.Background = New ImageBrush(Cover) With {.Stretch = Stretch.Uniform}
                                                 End If
                                                 tb_songtitle.Text = If(String.IsNullOrEmpty(Title), "N/A", Title)
                                                 tb_songartist.Text = If(String.IsNullOrEmpty(Artist), "N/A", Artist)
                                                 tb_songtitle.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(1, New Duration(TimeSpan.FromMilliseconds(250))))
                                                 tb_songartist.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(1, New Duration(TimeSpan.FromMilliseconds(250))))
                                                 tb_songtitle.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(0, 0, 0, 50), New Duration(TimeSpan.FromMilliseconds(500))))
                                                 tb_songartist.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(0, 50, 0, 0), New Duration(TimeSpan.FromMilliseconds(500))))
                                                 Await Task.Delay(2000)
                                                 tb_songtitle.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(660, 50, 0, 0), New Duration(TimeSpan.FromMilliseconds(500))))
                                                 tb_songartist.BeginAnimation(MarginProperty, New Animation.ThicknessAnimation(New Thickness(0, 0, 660, 50), New Duration(TimeSpan.FromMilliseconds(500))))
                                                 If Cover IsNot Nothing Then
                                                     LevelField.Background.BeginAnimation(Brush.OpacityProperty, New Animation.DoubleAnimation(0, New Duration(TimeSpan.FromMilliseconds(250))))
                                                 End If
                                                 Await Task.Delay(500)
                                                 tb_songtitle.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(0, New Duration(TimeSpan.FromMilliseconds(250))))
                                                 tb_songartist.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(0, New Duration(TimeSpan.FromMilliseconds(250))))
                                                 If Cover IsNot Nothing Then
                                                     LevelField.Background = New SolidColorBrush(Utils.GetAverageColor(Cover))
                                                 Else
                                                     LevelField.Background = Brushes.DodgerBlue
                                                 End If
                                                 VisUpdater.Start()
                                             End Sub
        LvGen.GenStart()
        'End If
    End Sub
    Private Async Sub Boom()
        Dim BSize = tb_score.FontSize
        tb_score.BeginAnimation(FontSizeProperty, New Animation.DoubleAnimation(BSize + 10, New Duration(TimeSpan.FromMilliseconds(100))))
        Await Task.Delay(100)
        tb_score.BeginAnimation(FontSizeProperty, New Animation.DoubleAnimation(BSize, New Duration(TimeSpan.FromMilliseconds(50))))
    End Sub

    Private Sub LvGen_OnScoreChanged(NewVal As Integer) Handles LvGen.OnScoreChanged
        tb_score.Text = "Score: " & NewVal
        Boom()
        If NewVal >= 250 AndAlso NewVal < 500 Then
            LevelField.Background.BeginAnimation(SolidColorBrush.ColorProperty, New Animation.ColorAnimation(Colors.LightGreen, New Duration(TimeSpan.FromMilliseconds(250))))
            VisColor1 = System.Drawing.Color.LightPink
            VisColor2 = System.Drawing.Color.Violet
        ElseIf NewVal >= 500 AndAlso NewVal < 50 Then
            LevelField.Background.BeginAnimation(SolidColorBrush.ColorProperty, New Animation.ColorAnimation(Colors.Orange, New Duration(TimeSpan.FromMilliseconds(250))))
            VisColor1 = System.Drawing.Color.DarkBlue
            VisColor2 = System.Drawing.Color.LightBlue
        ElseIf NewVal >= 300 AndAlso NewVal < 400 Then
            LevelField.Background.BeginAnimation(SolidColorBrush.ColorProperty, New Animation.ColorAnimation(Colors.Pink, New Duration(TimeSpan.FromMilliseconds(250))))
            VisColor1 = System.Drawing.Color.Turquoise
            VisColor2 = System.Drawing.Color.DarkTurquoise
        ElseIf NewVal >= 400 AndAlso NewVal < 500 Then
            LevelField.Background.BeginAnimation(SolidColorBrush.ColorProperty, New Animation.ColorAnimation(Colors.RosyBrown, New Duration(TimeSpan.FromMilliseconds(250))))
            VisColor1 = System.Drawing.Color.Cyan
            VisColor2 = System.Drawing.Color.DarkCyan
        ElseIf NewVal >= 500 AndAlso NewVal < 600 Then
            LevelField.Background.BeginAnimation(SolidColorBrush.ColorProperty, New Animation.ColorAnimation(Colors.DeepSkyBlue, New Duration(TimeSpan.FromMilliseconds(250))))
            VisColor1 = System.Drawing.Color.Red
            VisColor2 = System.Drawing.Color.DarkRed
        End If
    End Sub

    Private Sub LvGen_OnDifficultyChanged(NewVal As Integer) Handles LvGen.OnDifficultyChanged
        Select Case NewVal
            Case 3000
                tb_multiplier.Text = "Multiplier: X1"
                LvGen.BaseScore = 1
            Case 2500
                tb_multiplier.Text = "Multiplier: X2"
                LvGen.BaseScore = 2
            Case 2000
                tb_multiplier.Text = "Multiplier: X3"
                LvGen.BaseScore = 3
            Case 1500
                tb_multiplier.Text = "Multiplier: X4"
                LvGen.BaseScore = 4
            Case 1000
                tb_multiplier.Text = "Multiplier: X5"
                LvGen.BaseScore = 5
            Case 750
                tb_multiplier.Text = "Multiplier: X6"
                LvGen.BaseScore = 6
            Case 500
                tb_multiplier.Text = "Multiplier: X7"
                LvGen.BaseScore = 7
            Case 250
                tb_multiplier.Text = "Multiplier: X8"
                LvGen.BaseScore = 8
            Case 100
                tb_multiplier.Text = "Multiplier: X9"
                LvGen.BaseScore = 9
            Case 50
                tb_multiplier.Text = "Multiplier: X10"
                LvGen.BaseScore = 10
        End Select
    End Sub

    Private Async Sub LvGen_OnBoostAccuired(BoostType As Tile.TileBoostType) Handles LvGen.OnBoostAccuired
        tb_boost.Text = BoostType.ToString
        tb_boost.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(1, New Duration(TimeSpan.FromMilliseconds(150))))
        Await Task.Delay(1000)
        tb_boost.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(0, New Duration(TimeSpan.FromMilliseconds(150))))
    End Sub

    Private Sub LvGen_OnGameWon() Handles LvGen.OnGameWon
        MsgBox("Won!")
    End Sub

    Private Sub LvGen_OnGameOver() Handles LvGen.OnGameOver
        LvGen.GenStop()
        MsgBox("Lose!")
    End Sub

    Private Async Sub LvGen_OnWarningChanged(NewVal As Integer) Handles LvGen.OnWarningChanged
        Dim BC = TryCast(LevelField.Background, SolidColorBrush).Color
        LevelField.Background.BeginAnimation(SolidColorBrush.ColorProperty, New Animation.ColorAnimation(Colors.Red, New Duration(TimeSpan.FromMilliseconds(100))))
        tb_boost.Text = (3 - NewVal) & "Misses remaining"
        tb_boost.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(1, New Duration(TimeSpan.FromMilliseconds(150))))
        Await Task.Delay(100)
        LevelField.Background.BeginAnimation(SolidColorBrush.ColorProperty, New Animation.ColorAnimation(BC, New Duration(TimeSpan.FromMilliseconds(100))))
        Await Task.Delay(900)
        tb_boost.BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(0, New Duration(TimeSpan.FromMilliseconds(150))))
    End Sub
End Class
