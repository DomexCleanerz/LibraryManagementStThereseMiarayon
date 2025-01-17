﻿Imports Transitions

Public Class SlidePanel
    Public Event Closed As EventHandler
    Public Event Shown As EventHandler


    Private Sub owner_Resize(sender As Object, e As EventArgs)
        ResizeForm()

    End Sub

    Private Sub ResizeForm()
        Me.Width = _owner.Width
        Me.Height = _owner.Height - 77
        Me.Location = New Point(If(_loaded, 0, _owner.Width), 50)


    End Sub


    Public Sub swipe(Optional ByVal show As Boolean = False)
        Me.Visible = True
        Dim _transasition As Transition = New Transitions.Transition(New TransitionType_EaseInEaseOut(500))
        _transasition.add(Me, "Left", IIf(show, 0, Me.Width))
        _transasition.run()

        While Me.Left <> IIf(show, 0, Me.Width)
            Application.DoEvents()
        End While

        If Not show Then
            RaiseEvent Closed(Me, New EventArgs)
            RemoveHandler _owner.Resize, AddressOf owner_Resize
            _owner.Controls.Remove(Me)
            Me.Dispose()
        Else
            _loaded = True
            ResizeForm()
            RaiseEvent Shown(Me, New EventArgs)
        End If

    End Sub

    Private Sub SlidePanel_Click(sender As Object, e As EventArgs) Handles Me.Click
        swipe(False)
    End Sub

    Private Sub SlidePanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
