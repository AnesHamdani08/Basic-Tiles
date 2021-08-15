Class MainWindow

    Private Sub MainWindow_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Key = Key.A Then
            Dim GG As New HomeScreen
            CType(Me.Content, Grid).Children.Add(GG)
        End If
    End Sub
End Class
