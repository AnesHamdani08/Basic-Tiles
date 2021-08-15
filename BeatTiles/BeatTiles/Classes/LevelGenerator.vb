Public Class LevelGenerator

    Public Event OnScoreChanged(NewVal As Integer)
    Public Event OnDestroyedChanged(NewVal As Integer)
    Public Event OnDifficultyChanged(NewVal As Integer)
    Public Event OnBoostAccuired(BoostType As Tile.TileBoostType)
    Public Event OnWarningChanged(NewVal As Integer)
    Public Event OnGameOver()
    Public Event OnGameWon()

    Public Property Difficulty As Integer = 3000
    Public Property Lanes As Grid()
    Public Property Score As Integer
    Public Property TotalHits As Integer
    Public Property TotalDestroyed As Integer
    Public Property IsGenerating As Boolean
        Get
            Return _IsGenerating
        End Get
        Set(value As Boolean)
            _IsGenerating = value
            TilesGenerator.Enabled = value
        End Set
    End Property
    Public Property TotalWarnings = 0
    Public Property BaseScore As Integer = 1
    Public Property Player As New Player(Application.Current.MainWindow, AddressOf Player_MediaEnded) With {.AutoPlay = True}
    Public Property SongFile As String

    Private WithEvents DifficultyUpdator As New Forms.Timer With {.Interval = 50}
    Private WithEvents TilesGenerator As New Forms.Timer With {.Interval = 1100}
    Private LaneSelector As New Random
    Private BoostSelector As New Random
    Private _IsGenerating As Boolean = False

    Public Sub GenStart()
        If IO.File.Exists(SongFile) Then Player.LoadSong(SongFile)
        TilesGenerator.Start()
        DifficultyUpdator.Start()
    End Sub
    Public Sub GenStop()
        Player.StreamStop()
        TilesGenerator.Stop()
        DifficultyUpdator.Stop()
    End Sub

    Private Function CreateTile(Owner As Grid) As Tile
        Dim Boost = CreateBoost()
        If Boost = Tile.TileBoostType.NoBoost Then
            Return New Tile(Owner) With {.OnClick = Sub(_Score As Integer)
                                                        Score += _Score
                                                        RaiseEvent OnScoreChanged(Score)
                                                    End Sub, .OnDestroy = Sub()
                                                                              TotalDestroyed += 1
                                                                              RaiseEvent OnDestroyedChanged(TotalDestroyed)
                                                                          End Sub, .BoostType = Boost, .LVGen = Me}
        ElseIf Boost = Tile.TileBoostType.MultiHits Then
            Return New Tile(Owner) With {.OnClick = Sub(_Score As Integer)
                                                        Score += _Score
                                                        RaiseEvent OnScoreChanged(Score)
                                                        RaiseEvent OnBoostAccuired(Boost)
                                                    End Sub, .OnDestroy = Sub()
                                                                              TotalDestroyed += 1
                                                                              RaiseEvent OnDestroyedChanged(TotalDestroyed)
                                                                          End Sub, .BoostType = Boost, .RequiredHits = BoostSelector.Next(2, 6), .LVGen = Me}
        Else
            Return New Tile(Owner) With {.OnClick = Sub(_Score As Integer)
                                                        Score += _Score
                                                        RaiseEvent OnScoreChanged(Score)
                                                        RaiseEvent OnBoostAccuired(Boost)
                                                    End Sub, .OnDestroy = Sub()
                                                                              TotalDestroyed += 1
                                                                              RaiseEvent OnDestroyedChanged(TotalDestroyed)
                                                                          End Sub, .BoostType = Boost, .LVGen = Me}
        End If
    End Function
    Private Function CreateBoost() As Tile.TileBoostType
        Dim boost = BoostSelector.Next(0, 11)
        Select Case boost
            Case 7
                Return Tile.TileBoostType.MultiHits
            Case 8
                Return Tile.TileBoostType.X2Score
            Case 9
                Return Tile.TileBoostType.ScoreBoost
            Case 10
                If Difficulty >= 50 Then
                    Return Tile.TileBoostType.NoBoost
                Else
                    Return Tile.TileBoostType.SlowDifficulty
                End If
            Case Else
                Return Tile.TileBoostType.NoBoost
        End Select
    End Function

    Public Sub SignalNonClicked()
        TotalWarnings += 1
        RaiseEvent OnWarningChanged(TotalWarnings)
        If TotalWarnings = 3 Then
            RaiseEvent OnGameOver()
        End If
    End Sub

    Private Sub Player_MediaEnded()
        RaiseEvent OnGameWon()
    End Sub

    Private Sub TilesGenerator_Tick(sender As Object, e As EventArgs) Handles TilesGenerator.Tick
        Dim sel = LaneSelector.Next(1, 5)
        Select Case sel
            Case 1
                Lanes(0).Children.Add(CreateTile(Lanes(0)))
            Case 2
                Lanes(1).Children.Add(CreateTile(Lanes(1)))
            Case 3
                Lanes(2).Children.Add(CreateTile(Lanes(2)))
            Case 4
                Lanes(3).Children.Add(CreateTile(Lanes(3)))
        End Select
    End Sub

    Private Sub DifficultyUpdator_Tick(sender As Object, e As EventArgs) Handles DifficultyUpdator.Tick
        Difficulty -= 1 : RaiseEvent OnDifficultyChanged(Difficulty)
        If Difficulty = 0 Then
            RaiseEvent OnGameWon()
            Player.StreamStop()
            DifficultyUpdator.Stop()
            TilesGenerator.Stop()
        End If
    End Sub
End Class
