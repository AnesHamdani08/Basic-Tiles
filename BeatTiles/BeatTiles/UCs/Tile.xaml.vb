Public Class Tile
    Public Property OnClick As Action(Of Integer)
    Public Property OnDestroy As Action
    Public Property Owner As Grid
    Public Property ClickLine As FrameworkElement
    Public Property LVGen As LevelGenerator
    Public Property BoostType As TileBoostType = TileBoostType.NoBoost
    Public Property BoostIncrement As Integer = 50
    Public Property RequiredHits As Integer = 0
    Public Property IsClicked As Boolean = False
    Public Property OnClickDestroy As Boolean = False
    Public Sub New(Parent As FrameworkElement)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.        
        Owner = Parent
        Margin = New Thickness(0, -(Owner.ActualHeight + 150), 0, 0)
    End Sub
    Private Sub Tile_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles Me.MouseDown
        If IsClicked = False Then
            Select Case BoostType
                Case TileBoostType.NoBoost
                    Tile1.Background = Brushes.DarkGray
                    OnClick.Invoke(LVGen.BaseScore)
                    IsClicked = True
                Case TileBoostType.MultiHits
                    If tb_hits.Text = 1 Then
                        tb_hits.Text = String.Empty
                        Tile1.Effect = Nothing
                        Tile1.Background = Brushes.DarkGray
                        OnClick.Invoke(LVGen.BaseScore + RequiredHits)
                        IsClicked = True
                    ElseIf tb_hits.Text > 1 Then
                        tb_hits.Text -= 1
                    Else
                        tb_hits.Text = String.Empty
                        Tile1.Effect = Nothing
                        Tile1.Background = Brushes.DarkGray
                        OnClick.Invoke(LVGen.BaseScore + RequiredHits)
                        IsClicked = True
                    End If
                Case TileBoostType.ScoreBoost
                    Tile1.Background = Brushes.DarkGreen
                    LVGen.Score += BoostIncrement
                    OnClick.Invoke(LVGen.BaseScore)
                    IsClicked = True
                Case TileBoostType.SlowDifficulty
                    Tile1.Background = Brushes.LightYellow
                    OnClick.Invoke(LVGen.BaseScore)
                    If LVGen.Difficulty > 15 AndAlso (LVGen.Difficulty * 2 <= 60) Then
                        LVGen.Difficulty *= 2
                    Else
                        LVGen.Difficulty = 60
                    End If
                    IsClicked = True
                Case TileBoostType.X2Score
                    Tile1.Background = Brushes.DarkBlue
                    LVGen.BaseScore *= 2
                    OnClick.Invoke(LVGen.BaseScore)
                    IsClicked = True
            End Select
            If OnClickDestroy Then Owner.Children.Remove(Me)
        End If
    End Sub

    Private Sub Tile_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Select Case BoostType
            Case TileBoostType.NoBoost
                Tile1.Background = Brushes.Black
            Case TileBoostType.MultiHits
                tb_hits.Text = RequiredHits
                tb_hits.Visibility = Visibility.Visible
                Tile1.Effect = New Effects.DropShadowEffect With {.BlurRadius = 0, .Color = Colors.Red, .ShadowDepth = 0, .RenderingBias = Effects.RenderingBias.Performance}
                CType(Tile1.Effect, Effects.DropShadowEffect).BeginAnimation(Effects.DropShadowEffect.BlurRadiusProperty, New Animation.DoubleAnimation(0, 20, New Duration(TimeSpan.FromMilliseconds(100))) With {.AutoReverse = True, .Duration = Duration.Forever})
            Case TileBoostType.ScoreBoost
                Tile1.Background = Brushes.Green
            Case TileBoostType.SlowDifficulty
                Tile1.Background = Brushes.Yellow
            Case TileBoostType.X2Score
                Tile1.Background = Brushes.DodgerBlue
        End Select
        Dim FallAnimation = New Animation.ThicknessAnimation(New Thickness(0, 660, 0, 0), New Duration(TimeSpan.FromMilliseconds(If(BoostType <> TileBoostType.MultiHits, LVGen.Difficulty, LVGen.Difficulty * (1.5)))))
        AddHandler FallAnimation.Completed, Sub()
                                                If OnDestroy IsNot Nothing Then OnDestroy.Invoke
                                                If (IsClicked = False AndAlso (BoostType = TileBoostType.NoBoost Or BoostType = TileBoostType.MultiHits)) Then LVGen.SignalNonClicked()
                                                Owner.Children.Remove(Me)
                                            End Sub
        BeginAnimation(MarginProperty, FallAnimation)
    End Sub

    Public Enum TileBoostType
        NoBoost = 0
        MultiHits = 7
        X2Score = 8
        ScoreBoost = 9
        SlowDifficulty = 10
    End Enum
End Class
