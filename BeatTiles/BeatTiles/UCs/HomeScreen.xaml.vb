Public Class HomeScreen
    Private Sub btn_exit_Click(sender As Object, e As RoutedEventArgs) Handles btn_exit.Click
        Application.Current.Shutdown()
    End Sub

    Private Sub btn_play_Click(sender As Object, e As RoutedEventArgs) Handles btn_play.Click
        Dim GG As New LevelField
        CType(Application.Current.MainWindow.Content, Grid).Children.Remove(Me)
        CType(Application.Current.MainWindow.Content, Grid).Children.Add(GG)
        GG.StartLevel()
    End Sub

    Private Sub HomeScreen_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        AnimateUI()
    End Sub
    Private Async Sub AnimateUI()
        For i As Integer = 0 To Math.Truncate((BG.ActualHeight / 25))
            BG.Children.Add(New Rectangle() With {.Width = 250, .Height = 25, .Fill = Brushes.White, .Stroke = Brushes.Black, .StrokeThickness = 1})
        Next
        Dispatcher.BeginInvoke(Async Sub()
                                   tb_title.RenderTransform = New RotateTransform(0, tb_title.ActualWidth / 2, tb_title.ActualHeight / 2)
                                   Do While True
                                       tb_title.BeginAnimation(FontSizeProperty, New Animation.DoubleAnimation(80, New Duration(TimeSpan.FromMilliseconds(500))))
                                       CType(tb_title.RenderTransform, RotateTransform).BeginAnimation(RotateTransform.AngleProperty, New Animation.DoubleAnimation(10, New Duration(TimeSpan.FromMilliseconds(500))))
                                       Await Task.Delay(500)
                                       tb_title.BeginAnimation(FontSizeProperty, New Animation.DoubleAnimation(64, New Duration(TimeSpan.FromMilliseconds(500))))
                                       CType(tb_title.RenderTransform, RotateTransform).BeginAnimation(RotateTransform.AngleProperty, New Animation.DoubleAnimation(-10, New Duration(TimeSpan.FromMilliseconds(500))))
                                       Await Task.Delay(500)
                                   Loop
                               End Sub)
        Dispatcher.BeginInvoke(Async Sub()
                                   Dim RND As New Random
                                   Do While True
                                       Dim SelCTRL = RND.Next(0, BG.Children.Count)
                                       CType(BG.Children.Item(SelCTRL), Rectangle).Fill = Brushes.Black
                                       Await Task.Delay(250)
                                       CType(BG.Children.Item(SelCTRL), Rectangle).Fill = Brushes.White
                                   Loop
                               End Sub)
    End Sub
End Class
