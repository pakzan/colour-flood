Imports System.Math
Public Class Form1
    Dim x, y, a, b As Integer
    Dim ply1, ply2, comp1, comp2, hold As Integer
    Dim lb As Integer
    Dim rnd As New Random
    Dim colourM(50, 50), ignoreM(50, 50) As Byte
    Dim matrixM(50, 50) As Label
    Dim checkM(50, 50) As Label
    Dim typeM(50, 50) As Byte
    Dim num, count, start As Byte
    Dim clabel(5), plabel(5) As Label
    Private Sub SelectBlockNumberToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectBlockNumberToolStripMenuItem.Click
        Form2.Visible = True
    End Sub

    Private Sub Form1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDoubleClick
        For i = 0 To Val(num - 1)
            For j = 0 To Val(num - 1)
                matrixM(i, j).Text = typeM(i, j)
            Next
        Next
    End Sub
    Private Sub Form1_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel
        For i = 0 To Val(num - 1)
            For j = 0 To Val(num - 1)
                matrixM(i, j).Text = matrixM(i, j).Text + checkM(i, j).Text
            Next
        Next
        clabel(0) = Label1
        clabel(1) = Label2
        clabel(2) = Label3
        clabel(3) = Label4
        clabel(4) = Label5
        clabel(5) = Label15
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label1.BackColor = Color.Brown
        Label2.BackColor = Color.DarkOrange
        Label3.BackColor = Color.Gold
        Label4.BackColor = Color.LimeGreen
        Label5.BackColor = Color.DeepSkyBlue
        Label15.BackColor = Color.BlueViolet

        Label1.Size = New Size(Me.Width / 9, Me.Height / 9)
        Label2.Size = New Size(Me.Width / 9, Me.Height / 9)
        Label3.Size = New Size(Me.Width / 9, Me.Height / 9)
        Label4.Size = New Size(Me.Width / 9, Me.Height / 9)
        Label5.Size = New Size(Me.Width / 9, Me.Height / 9)
        Label15.Size = New Size(Me.Width / 9, Me.Height / 9)

        Label1.Location = New Point(8 * Me.Width / 10, 1 * Me.Height / 10 + 5)
        Label2.Location = New Point(8 * Me.Width / 10, 2 * Me.Height / 10 + 5)
        Label3.Location = New Point(8 * Me.Width / 10, 3 * Me.Height / 10 + 5)
        Label4.Location = New Point(8 * Me.Width / 10, 4 * Me.Height / 10 + 5)
        Label5.Location = New Point(8 * Me.Width / 10, 5 * Me.Height / 10 + 5)
        Label15.Location = New Point(8 * Me.Width / 10, 6 * Me.Height / 10 + 5)
        Label10.Location = New Point(7 * Me.Width / 20, 30)
    End Sub
    Private Sub Form1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Label1.Size = New Size(Me.Width / 9, Me.Height / 9)
        Label2.Size = New Size(Me.Width / 9, Me.Height / 9)
        Label3.Size = New Size(Me.Width / 9, Me.Height / 9)
        Label4.Size = New Size(Me.Width / 9, Me.Height / 9)
        Label5.Size = New Size(Me.Width / 9, Me.Height / 9)
        Label15.Size = New Size(Me.Width / 9, Me.Height / 9)

        Label1.Location = New Point(8 * Me.Width / 10, 1 * Me.Height / 10 + 5)
        Label2.Location = New Point(8 * Me.Width / 10, 2 * Me.Height / 10 + 5)
        Label3.Location = New Point(8 * Me.Width / 10, 3 * Me.Height / 10 + 5)
        Label4.Location = New Point(8 * Me.Width / 10, 4 * Me.Height / 10 + 5)
        Label5.Location = New Point(8 * Me.Width / 10, 5 * Me.Height / 10 + 5)
        Label15.Location = New Point(8 * Me.Width / 10, 6 * Me.Height / 10 + 5)
        Label10.Location = New Point(7 * Me.Width / 20, 30)
        If a = 0 Then
            Return
        Else

            If Me.Height >= Me.Width Then
                lb = (Me.Width - 200) / Val(num)
            ElseIf Me.Width > Me.Height Then
                lb = (Me.Height - 200) / Val(num)
            End If

            For j = 0 To Val(num - 1)
                For i = 0 To Val(num - 1)
                    Dim var As New Label
                    x = (Me.Width - Val(num) * lb) / 2 + i * lb - 50
                    y = (Me.Height - Val(num) * lb) / 2 + j * lb - 10
                    Me.Controls.Remove(matrixM(i, j))
                    var.Location = New Point(x, y)
                    var.Size = New Size(lb + 1, lb + 1)

                    a = colourM(i, j)
                    Select Case a
                        Case Is = 1
                            var.BackColor = Label1.BackColor
                        Case Is = 2
                            var.BackColor = Label2.BackColor
                        Case Is = 3
                            var.BackColor = Label3.BackColor
                        Case Is = 4
                            var.BackColor = Label4.BackColor
                        Case Is = 5
                            var.BackColor = Label5.BackColor
                        Case Is = 6
                            var.BackColor = Label15.BackColor
                        Case Is = 7
                            var.BackColor = Color.White
                    End Select
                    matrixM(i, j) = var
                    matrixM(i, j).Text = typeM(i, j)
                    Me.Controls.Add(matrixM(i, j))
                Next
            Next
        End If

    End Sub
    Private Sub reset()
        b = 0
        count = 0
        hold = 0
        start = 0

        Label6.BackColor = Color.Black
        Label7.BackColor = Color.Black
        Label10.Text = ""
        For i = 0 To Val(num - 1)
            For j = 0 To Val(num - 1)
                Me.Controls.Remove(matrixM(i, j))
            Next
        Next
        num = Val(Form2.TextBox1.Text)
        If num <= 0 Then
            Label1.Enabled = False
            Label2.Enabled = False
            Label3.Enabled = False
            Label4.Enabled = False
            Label5.Enabled = False
            Label15.Enabled = False
            Return
        Else
            Label1.Enabled = True
            Label2.Enabled = True
            Label3.Enabled = True
            Label4.Enabled = True
            Label5.Enabled = True
            Label15.Enabled = True
        End If

        If Me.Height >= Me.Width Then
            lb = (Me.Width - 200) / Val(num)
        ElseIf Me.Width > Me.Height Then
            lb = (Me.Height - 200) / Val(num)
        End If
        For j = 0 To Val(num - 1)
            For i = 0 To Val(num - 1)
                typeM(i, j) = 0
            Next
        Next
        typeM(0, 0) = 1
        typeM(Val(num - 1), Val(num - 1)) = 2
        For j = 0 To Val(num - 1)
            For i = 0 To Val(num - 1)
                Dim var As New Label
                x = (Me.Width - Val(num) * lb) / 2 + i * lb - 50
                y = (Me.Height - Val(num) * lb) / 2 + j * lb - 10
                var.Location = New Point(x, y)
                var.Size = New Size(lb + 1, lb + 1)
                If 0 < i And i < Val(num - 1) And 0 < j Then
                    Do
                        a = rnd.Next(1, 7)
                        colourM(i, j) = a
                        Select Case colourM(i, j)
                            Case Is = 1
                                var.BackColor = Label1.BackColor
                            Case Is = 2
                                var.BackColor = Label2.BackColor
                            Case Is = 3
                                var.BackColor = Label3.BackColor
                            Case Is = 4
                                var.BackColor = Label4.BackColor
                            Case Is = 5
                                var.BackColor = Label5.BackColor
                            Case Is = 6
                                var.BackColor = Label15.BackColor
                            Case Is = 7
                                var.BackColor = Color.White
                        End Select
                    Loop Until colourM(i, j) <> colourM(i, j - 1) And colourM(i, j) <> colourM(i - 1, j) And colourM(i, j) <> colourM(i - 1, j - 1) And colourM(i, j) <> colourM(i + 1, j - 1)

                ElseIf i = 0 And j > 0 Then
                    Do
                        a = rnd.Next(1, 7)
                        colourM(i, j) = a
                        colourM(Val(num - 1), Val(num - 1)) = 7
                        Select Case colourM(i, j)
                            Case Is = 1
                                var.BackColor = Label1.BackColor
                            Case Is = 2
                                var.BackColor = Label2.BackColor
                            Case Is = 3
                                var.BackColor = Label3.BackColor
                            Case Is = 4
                                var.BackColor = Label4.BackColor
                            Case Is = 5
                                var.BackColor = Label5.BackColor
                            Case Is = 6
                                var.BackColor = Label15.BackColor
                            Case Is = 7
                                var.BackColor = Color.White
                        End Select
                    Loop Until colourM(i, j) <> colourM(i, j - 1) And colourM(i, j) <> colourM(i + 1, j - 1)
                ElseIf i = Val(num - 1) And j > 0 Then
                    Do
                        a = rnd.Next(1, 7)
                        colourM(i, j) = a
                        colourM(Val(num - 1), Val(num - 1)) = 7
                        Select Case colourM(i, j)
                            Case Is = 1
                                var.BackColor = Label1.BackColor
                            Case Is = 2
                                var.BackColor = Label2.BackColor
                            Case Is = 3
                                var.BackColor = Label3.BackColor
                            Case Is = 4
                                var.BackColor = Label4.BackColor
                            Case Is = 5
                                var.BackColor = Label5.BackColor
                            Case Is = 6
                                var.BackColor = Label15.BackColor
                            Case Is = 7
                                var.BackColor = Color.White
                        End Select
                    Loop Until colourM(i, j) <> colourM(i, j - 1) And colourM(i, j) <> colourM(i - 1, j)
                ElseIf i > 0 And j = 0 Then
                    Do
                        a = rnd.Next(1, 7)
                        colourM(i, j) = a
                        Select Case colourM(i, j)
                            Case Is = 1
                                var.BackColor = Label1.BackColor
                            Case Is = 2
                                var.BackColor = Label2.BackColor
                            Case Is = 3
                                var.BackColor = Label3.BackColor
                            Case Is = 4
                                var.BackColor = Label4.BackColor
                            Case Is = 5
                                var.BackColor = Label5.BackColor
                            Case Is = 6
                                var.BackColor = Label15.BackColor
                            Case Is = 7
                                var.BackColor = Color.White
                        End Select
                    Loop Until colourM(i, j) <> colourM(i - 1, j)
                Else
                    colourM(0, 0) = 7
                    var.BackColor = Color.White
                End If
                matrixM(i, j) = var
                matrixM(i, j).Text = typeM(i, j)
                Me.Controls.Add(matrixM(i, j))
            Next
        Next
        Timer1.Stop()
        Timer2.Start()
        Label8.Text = "Your score = 0"
        Label9.Text = "Computer score = 0"
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click
        If Label7.BackColor = Label1.BackColor Then
            Label10.Text = "Chosen colour same with computer's!" + Chr(13) + "        Please choose another colour."
            Timer2.Start()
            Return
        End If
        If Label6.BackColor = Label1.BackColor Then
            Return
        End If
        Label6.BackColor = Label1.BackColor
        player()
        Label12.Text = Label12.Text + "a"
        Timer1.Start()
        If Form2.CheckBox2.Checked = True And b = 0 Then
            comp()
        End If
        For i = 0 To Val(num - 1)
            For j = 0 To Val(num - 1)
                If matrixM(i, j).Text = "1" Then
                    colourM(i, j) = 1
                End If
            Next
        Next

    End Sub
    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click
        If Label7.BackColor = Label2.BackColor Then
            Label10.Text = "Chosen colour same with computer's!" + Chr(13) + "        Please choose another colour."
            Timer2.Start()
            Return
        End If
        If Label6.BackColor = Label2.BackColor Then
            Return
        End If
        Label6.BackColor = Label2.BackColor
        player()
        Label12.Text = Label12.Text + "a"
        Timer1.Start()
        If Form2.CheckBox2.Checked = True And b = 0 Then
            comp()
        End If
        For i = 0 To Val(num - 1)
            For j = 0 To Val(num - 1)
                If matrixM(i, j).Text = "1" Then
                    colourM(i, j) = 2
                End If
            Next
        Next
    End Sub
    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click
        If Label7.BackColor = Label3.BackColor Then
            Label10.Text = "Chosen colour same with computer's!" + Chr(13) + "        Please choose another colour."
            Timer2.Start()
            Return
        End If
        If Label6.BackColor = Label3.BackColor Then
            Return
        End If
        Label6.BackColor = Label3.BackColor
        player()
        Label12.Text = Label12.Text + "a"
        Timer1.Start()
        If Form2.CheckBox2.Checked = True And b = 0 Then
            comp()
        End If
        For i = 0 To Val(num - 1)
            For j = 0 To Val(num - 1)
                If matrixM(i, j).Text = "1" Then
                    colourM(i, j) = 3
                End If
            Next
        Next
    End Sub
    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click
        If Label7.BackColor = Label4.BackColor Then
            Label10.Text = "Chosen colour same with computer's!" + Chr(13) + "        Please choose another colour."
            Timer2.Start()
            Return
        End If
        If Label6.BackColor = Label4.BackColor Then
            Return
        End If
        Label6.BackColor = Label4.BackColor
        player()
        Label12.Text = Label12.Text + "a"
        Timer1.Start()
        If Form2.CheckBox2.Checked = True And b = 0 Then
            comp()
        End If
        For i = 0 To Val(num - 1)
            For j = 0 To Val(num - 1)
                If matrixM(i, j).Text = "1" Then
                    colourM(i, j) = 4
                End If
            Next
        Next
    End Sub
    Private Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label5.Click
        If Label7.BackColor = Label5.BackColor Then
            Label10.Text = "Chosen colour same with computer's!" + Chr(13) + "        Please choose another colour."
            Timer2.Start()
            Return
        End If
        If Label6.BackColor = Label5.BackColor Then
            Return
        End If
        Label6.BackColor = Label5.BackColor
        player()
        Label12.Text = Label12.Text + "a"
        Timer1.Start()
        If Form2.CheckBox2.Checked = True And b = 0 Then
            comp()
        End If
        For i = 0 To Val(num - 1)
            For j = 0 To Val(num - 1)
                If matrixM(i, j).Text = "1" Then
                    colourM(i, j) = 5
                End If
            Next
        Next
    End Sub
    Private Sub Label15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label15.Click
        If Label7.BackColor = Label15.BackColor Then
            Label10.Text = "Chosen colour same with computer's!" + Chr(13) + "        Please choose another colour."
            Timer2.Start()
            Return
        End If
        If Label6.BackColor = Label15.BackColor Then
            Return
        End If
        Label6.BackColor = Label15.BackColor
        player()
        Label12.Text = Label12.Text + "a"
        Timer1.Start()
        If Form2.CheckBox2.Checked = True And b = 0 Then
            comp()
        End If
        For i = 0 To Val(num - 1)
            For j = 0 To Val(num - 1)
                If matrixM(i, j).Text = "1" Then
                    colourM(i, j) = 6
                End If
            Next
        Next
    End Sub

    Private Sub player()
        For i = 0 To Val(num - 1)
            For j = 0 To Val(num - 1)
                If matrixM(i, j).Text = "1" Then
                    If i > 0 Then
                        If matrixM(i - 1, j).Text = "0" And matrixM(i - 1, j).BackColor = Label6.BackColor Then
                            matrixM(i - 1, j).Text = "1"
                        End If
                    End If
                    If i < Val(num - 1) Then
                        If matrixM(i + 1, j).Text = "0" And matrixM(i + 1, j).BackColor = Label6.BackColor Then
                            matrixM(i + 1, j).Text = "1"
                        End If
                    End If
                    If j > 0 Then
                        If matrixM(i, j - 1).Text = "0" And matrixM(i, j - 1).BackColor = Label6.BackColor Then
                            matrixM(i, j - 1).Text = "1"
                        End If
                    End If
                    If j < Val(num - 1) Then
                        If matrixM(i, j + 1).Text = "0" And matrixM(i, j + 1).BackColor = Label6.BackColor Then
                            matrixM(i, j + 1).Text = "1"
                        End If
                    End If
                End If
            Next
        Next
        For i = Val(num - 1) To 0 Step -1
            For j = Val(num - 1) To 0 Step -1
                If matrixM(i, j).Text = "1" Then
                    If i > 0 Then
                        If matrixM(i - 1, j).Text = "0" And matrixM(i - 1, j).BackColor = Label6.BackColor Then
                            matrixM(i - 1, j).Text = "1"
                        End If
                    End If
                    If i < Val(num - 1) Then
                        If matrixM(i + 1, j).Text = "0" And matrixM(i + 1, j).BackColor = Label6.BackColor Then
                            matrixM(i + 1, j).Text = "1"
                        End If
                    End If
                    If j > 0 Then
                        If matrixM(i, j - 1).Text = "0" And matrixM(i, j - 1).BackColor = Label6.BackColor Then
                            matrixM(i, j - 1).Text = "1"
                        End If
                    End If
                    If j < Val(num - 1) Then
                        If matrixM(i, j + 1).Text = "0" And matrixM(i, j + 1).BackColor = Label6.BackColor Then
                            matrixM(i, j + 1).Text = "1"
                        End If
                    End If
                End If
            Next
        Next
        For i = 0 To Val(num - 1)
            For j = 0 To Val(num - 1)
                If matrixM(i, j).Text = "1" Then
                    matrixM(i, j).BackColor = Label6.BackColor
                    typeM(i, j) = matrixM(i, j).Text
                End If
            Next
        Next

    End Sub
    Private Sub comp()
        For i = 0 To Val(num - 1)
            For j = 0 To Val(num - 1)
                Dim c As New Label
                checkM(i, j) = c
                checkM(i, j).Text = "0"
            Next
        Next
        choosecolor()
        For i = 0 To Val(num - 1)
            For j = 0 To Val(num - 1)
                If matrixM(i, j).Text = "2" Then
                    If i > 0 Then
                        If matrixM(i - 1, j).Text = "0" And matrixM(i - 1, j).BackColor = Label7.BackColor Then
                            matrixM(i - 1, j).Text = "2"
                        End If
                    End If
                    If i < Val(num - 1) Then
                        If matrixM(i + 1, j).Text = "0" And matrixM(i + 1, j).BackColor = Label7.BackColor Then
                            matrixM(i + 1, j).Text = "2"
                        End If
                    End If
                    If j > 0 Then
                        If matrixM(i, j - 1).Text = "0" And matrixM(i, j - 1).BackColor = Label7.BackColor Then
                            matrixM(i, j - 1).Text = "2"
                        End If
                    End If
                    If j < Val(num - 1) Then
                        If matrixM(i, j + 1).Text = "0" And matrixM(i, j + 1).BackColor = Label7.BackColor Then
                            matrixM(i, j + 1).Text = "2"
                        End If
                    End If
                End If
            Next
        Next
        For i = Val(num - 1) To 0 Step -1
            For j = Val(num - 1) To 0 Step -1
                If matrixM(i, j).Text = "2" Then
                    If i > 0 Then
                        If matrixM(i - 1, j).Text = "0" And matrixM(i - 1, j).BackColor = Label7.BackColor Then
                            matrixM(i - 1, j).Text = "2"
                        End If
                    End If
                    If i < Val(num - 1) Then
                        If matrixM(i + 1, j).Text = "0" And matrixM(i + 1, j).BackColor = Label7.BackColor Then
                            matrixM(i + 1, j).Text = "2"
                        End If
                    End If
                    If j > 0 Then
                        If matrixM(i, j - 1).Text = "0" And matrixM(i, j - 1).BackColor = Label7.BackColor Then
                            matrixM(i, j - 1).Text = "2"
                        End If
                    End If
                    If j < Val(num - 1) Then
                        If matrixM(i, j + 1).Text = "0" And matrixM(i, j + 1).BackColor = Label7.BackColor Then
                            matrixM(i, j + 1).Text = "2"
                        End If
                    End If
                End If
            Next
        Next
        For i = 0 To Val(num - 1)
            For j = 0 To Val(num - 1)
                If matrixM(i, j).Text = "2" Then
                    matrixM(i, j).BackColor = Label7.BackColor
                    typeM(i, j) = matrixM(i, j).Text
                    colour()
                End If
            Next
        Next
        checkwin()

    End Sub
    Private Sub choosecolor()

        For i = 0 To Val(num - 1)
            For j = 0 To Val(num - 1)
                If matrixM(i, j).Text = "0" Then
                    If i > 0 And i < Val(num - 1) Then
                        If matrixM(i - 1, j).Text = "0" And matrixM(i + 1, j).Text = "2" Then
                            checkM(i, j).Text = "2"
                        End If
                    End If
                    If j > 0 And j < Val(num - 1) Then
                        If matrixM(i, j - 1).Text = "0" And matrixM(i, j + 1).Text = "2" Then
                            checkM(i, j).Text = "2"
                        End If
                    End If
                End If
            Next
        Next

        For i = 0 To Val(num - 1)
            For j = 0 To Val(num - 1)
                If checkM(i, j).Text = "2" Then
                    If i > 2 And i < Val(num - 1) Then
                        If matrixM(i - 2, j).Text = "2" And matrixM(i + 1, j).Text = "2" Then
                            checkM(i, j).Text = "-2"
                        End If
                        If matrixM(i - 3, j).Text = "2" And matrixM(i + 1, j).Text = "2" Then
                            checkM(i, j).Text = "-2"
                        End If
                    End If
                    If j > 2 And j < Val(num - 1) Then
                        If matrixM(i, j - 2).Text = "2" And matrixM(i, j + 1).Text = "2" Then
                            checkM(i, j).Text = "-2"
                        End If
                        If matrixM(i, j - 3).Text = "2" And matrixM(i, j + 1).Text = "2" Then
                            checkM(i, j).Text = "-2"
                        End If
                    End If
                End If
            Next
        Next

        For i = 1 To Val(num - 2)
            For j = 1 To Val(num - 2)
                If checkM(i, j).Text = "2" Then
                    If matrixM(i - 1, j - 1).Text = "2" And matrixM(i + 1, j + 1).Text = "2" Or matrixM(i - 1, j + 1).Text = "2" And matrixM(i + 1, j - 1).Text = "2" Then
                        checkM(i, j).Text = "-2"
                    End If
                End If
            Next
        Next

        For j = 1 To Val(num - 2)
            If checkM(0, j).Text = "2" And matrixM(1, j + 1).Text = "2" And matrixM(1, j - 1).Text = "2" Then
                checkM(0, j).Text = "-2"
            End If
            If checkM(num - 1, j).Text = "2" And matrixM(num - 2, j + 1).Text = "2" And matrixM(num - 2, j - 1).Text = "2" Then
                checkM(num - 1, j).Text = "-2"
            End If
        Next
        For i = 1 To Val(num - 2)
            If checkM(i, 0).Text = "2" And matrixM(i + 1, 1).Text = "2" And matrixM(i - 1, 1).Text = "2" Then
                checkM(i, 0).Text = "-2"
            End If
            If checkM(i, num - 1).Text = "2" And matrixM(i + 1, num - 2).Text = "2" And matrixM(i - 1, num - 2).Text = "2" Then
                checkM(i, num - 1).Text = "-2"
            End If
        Next

        For i = 0 To Val(num - 1)
            For j = 0 To Val(num - 1)
                If matrixM(i, j).Text = "0" Then
                    If i > 0 And i < Val(num - 1) Then
                        If matrixM(i - 1, j).Text = "1" And matrixM(i + 1, j).Text = "2" Then
                            checkM(i, j).Text = "2"
                        End If
                    End If
                    If j > 0 And j < Val(num - 1) Then
                        If matrixM(i, j - 1).Text = "1" And matrixM(i, j + 1).Text = "2" Then
                            checkM(i, j).Text = "2"
                        End If
                    End If
                End If
            Next
        Next

        For i = 0 To Val(num - 1)
            For j = 0 To Val(num - 1)
                If checkM(i, j).Text = "2" Then
                    If i > 0 Then
                        If checkM(i - 1, j).Text = "0" And matrixM(i - 1, j).BackColor = matrixM(i, j).BackColor And matrixM(i - 1, j).Text = "0" Then
                            checkM(i - 1, j).Text = "2"
                        End If
                    End If
                    If i < Val(num - 1) Then
                        If checkM(i + 1, j).Text = "0" And matrixM(i + 1, j).BackColor = matrixM(i, j).BackColor And matrixM(i + 1, j).Text = "0" Then
                            checkM(i + 1, j).Text = "2"
                        End If
                    End If
                    If j > 0 Then
                        If checkM(i, j - 1).Text = "0" And matrixM(i, j - 1).BackColor = matrixM(i, j).BackColor And matrixM(i, j - 1).Text = "0" Then
                            checkM(i, j - 1).Text = "2"
                        End If
                    End If
                    If j < Val(num - 1) Then
                        If checkM(i, j + 1).Text = "0" And matrixM(i, j + 1).BackColor = matrixM(i, j).BackColor And matrixM(i, j + 1).Text = "0" Then
                            checkM(i, j + 1).Text = "2"
                        End If
                    End If
                End If
            Next
        Next
        For i = Val(num - 1) To 0 Step -1
            For j = Val(num - 1) To 0 Step -1
                If checkM(i, j).Text = "2" Then
                    If i > 0 Then
                        If checkM(i - 1, j).Text = "0" And matrixM(i - 1, j).BackColor = matrixM(i, j).BackColor And matrixM(i - 1, j).Text = "0" Then
                            checkM(i - 1, j).Text = "2"
                        End If
                    End If
                    If i < Val(num - 1) Then
                        If checkM(i + 1, j).Text = "0" And matrixM(i + 1, j).BackColor = matrixM(i, j).BackColor And matrixM(i + 1, j).Text = "0" Then
                            checkM(i + 1, j).Text = "2"
                        End If
                    End If
                    If j > 0 Then
                        If checkM(i, j - 1).Text = "0" And matrixM(i, j - 1).BackColor = matrixM(i, j).BackColor And matrixM(i, j - 1).Text = "0" Then
                            checkM(i, j - 1).Text = "2"
                        End If
                    End If
                    If j < Val(num - 1) Then
                        If checkM(i, j + 1).Text = "0" And matrixM(i, j + 1).BackColor = matrixM(i, j).BackColor And matrixM(i, j + 1).Text = "0" Then
                            checkM(i, j + 1).Text = "2"
                        End If
                    End If
                End If
            Next
        Next

        For i = 0 To Val(num - 1)
            For j = 0 To Val(num - 1)
                Dim ignore As New Byte
                ignore = 0
                ignoreM(i, j) = ignore
            Next
        Next
        For i = 0 To Val(num - 1)
            For j = 0 To Val(num - 1)
                If matrixM(i, j).Text = "2" Then
                    If i > 0 Then
                        If matrixM(i - 1, j).Text = "0" Then
                            ignoreM(i - 1, j) = 2
                        End If
                    End If
                    If i < Val(num - 1) Then
                        If matrixM(i + 1, j).Text = "0" Then
                            ignoreM(i + 1, j) = 2
                        End If
                    End If
                    If j > 0 Then
                        If matrixM(i, j - 1).Text = "0" Then
                            ignoreM(i, j - 1) = 2
                        End If
                    End If
                    If j < Val(num - 1) Then
                        If matrixM(i, j + 1).Text = "0" Then
                            ignoreM(i, j + 1) = 2
                        End If
                    End If
                End If
            Next
        Next

        clabel(0) = Label1
        clabel(1) = Label2
        clabel(2) = Label3
        clabel(3) = Label4
        clabel(4) = Label5
        clabel(5) = Label15
        clabel(0).Text = "0"
        clabel(1).Text = "0"
        clabel(2).Text = "0"
        clabel(3).Text = "0"
        clabel(4).Text = "0"
        clabel(5).Text = "0"
        For i = 0 To Val(num - 1)
            For j = 0 To Val(num - 1)
                If checkM(i, j).Text = "2" Then
                    Dim more As New Byte
                    If i > 0 Then
                        If matrixM(i - 1, j).Text = "0" And ignoreM(i - 1, j) <> 2 Then
                            more = more + 1
                        End If
                    End If
                    If i < Val(num - 1) Then
                        If matrixM(i + 1, j).Text = 0 And ignoreM(i + 1, j) <> 2 Then
                            more = more + 1
                        End If
                    End If
                    If j > 0 Then
                        If matrixM(i, j - 1).Text = 0 And ignoreM(i, j - 1) <> 2 Then
                            more = more + 1
                        End If
                    End If
                    If j < Val(num - 1) Then
                        If matrixM(i, j + 1).Text = 0 And ignoreM(i, j + 1) <> 2 Then
                            more = more + 1
                        End If
                    End If
                    Select Case matrixM(i, j).BackColor
                        Case Is = Label1.BackColor
                            clabel(0).Text = Val(clabel(0).Text) + Val(more)
                        Case Is = Label2.BackColor
                            clabel(1).Text = Val(clabel(1).Text) + Val(more)
                        Case Is = Label3.BackColor
                            clabel(2).Text = Val(clabel(2).Text) + Val(more)
                        Case Is = Label4.BackColor
                            clabel(3).Text = Val(clabel(3).Text) + Val(more)
                        Case Is = Label5.BackColor
                            clabel(4).Text = Val(clabel(4).Text) + Val(more)
                        Case Is = Label15.BackColor
                            clabel(5).Text = Val(clabel(5).Text) + Val(more)
                    End Select
                End If
            Next
        Next
        Select Case Label6.BackColor
            Case Is = Label1.BackColor
                clabel(0).Text = "0"
            Case Is = Label2.BackColor
                clabel(1).Text = "0"
            Case Is = Label3.BackColor
                clabel(2).Text = "0"
            Case Is = Label4.BackColor
                clabel(3).Text = "0"
            Case Is = Label5.BackColor
                clabel(4).Text = "0"
            Case Is = Label6.BackColor
                clabel(5).Text = "0"
        End Select

        For n = 0 To 5
            If Val(clabel(n).Text) >= Val(clabel(0).Text) And Val(clabel(n).Text) >= Val(clabel(1).Text) And Val(clabel(n).Text) >= Val(clabel(2).Text) And Val(clabel(n).Text) >= Val(clabel(3).Text) And Val(clabel(n).Text) >= Val(clabel(4).Text) And Val(clabel(n).Text) >= Val(clabel(5).Text) Then
                Label7.BackColor = clabel(n).BackColor
                Label7.Text = clabel(n).Text
                If clabel(n).Text = "0" Then
                    Dim g As New Byte
                    For i = 0 To Val(num - 1)
                        For j = 0 To Val(num - 1)
                            If matrixM(i, j).Text = "2" Then
                                If i > 0 Then
                                    If matrixM(i - 1, j).Text = "0" And matrixM(i - 1, j).BackColor <> matrixM(0, 0).BackColor And matrixM(i - 1, j).BackColor <> matrixM(num - 1, num - 1).BackColor Then
                                        Label7.BackColor = matrixM(i - 1, j).BackColor
                                        g = 1
                                    End If
                                End If
                                If i < Val(num - 1) Then
                                    If matrixM(i + 1, j).Text = "0" And matrixM(i + 1, j).BackColor <> matrixM(0, 0).BackColor And matrixM(i + 1, j).BackColor <> matrixM(num - 1, num - 1).BackColor Then
                                        Label7.BackColor = matrixM(i + 1, j).BackColor
                                        g = 1
                                    End If
                                End If
                                If j > 0 Then
                                    If matrixM(i, j - 1).Text = "0" And matrixM(i, j - 1).BackColor <> matrixM(0, 0).BackColor And matrixM(i, j - 1).BackColor <> matrixM(num - 1, num - 1).BackColor Then
                                        Label7.BackColor = matrixM(i, j - 1).BackColor
                                        g = 1
                                    End If
                                End If
                                If j < Val(num - 1) Then
                                    If matrixM(i, j + 1).Text = "0" And matrixM(i, j + 1).BackColor <> matrixM(0, 0).BackColor And matrixM(i, j + 1).BackColor <> matrixM(num - 1, num - 1).BackColor Then
                                        Label7.BackColor = matrixM(i, j + 1).BackColor
                                        g = 1
                                    End If
                                End If
                            End If
                        Next
                    Next

                    If g = 0 Then
                        Do
                            a = rnd.Next(1, 7)
                            Select Case a
                                Case Is = 1
                                    Label7.BackColor = Label1.BackColor
                                Case Is = 2
                                    Label7.BackColor = Label2.BackColor
                                Case Is = 3
                                    Label7.BackColor = Label3.BackColor
                                Case Is = 4
                                    Label7.BackColor = Label4.BackColor
                                Case Is = 5
                                    Label7.BackColor = Label5.BackColor
                                Case Is = 6
                                    Label7.BackColor = Label15.BackColor
                            End Select
                        Loop Until Label7.BackColor <> matrixM(0, 0).BackColor And Label7.BackColor <> matrixM(num - 1, num - 1).BackColor
                    End If
                End If
            End If
        Next

        strategyi()
        strategy2()
    End Sub
    Private Sub colour()
        For i = 0 To Val(num - 1)
            For j = 0 To Val(num - 1)
                If matrixM(i, j).Text = "2" Then
                    Select Case Label7.BackColor
                        Case Is = Label1.BackColor
                            colourM(i, j) = "1"
                        Case Is = Label2.BackColor
                            colourM(i, j) = "2"
                        Case Is = Label3.BackColor
                            colourM(i, j) = "3"
                        Case Is = Label4.BackColor
                            colourM(i, j) = "4"
                        Case Is = Label5.BackColor
                            colourM(i, j) = "5"
                        Case Is = Label15.BackColor
                            colourM(i, j) = "6"
                    End Select
                End If
            Next
        Next
    End Sub
    Private Sub checkwin()
        Dim plyscore As Integer
        Dim compscore As Integer
        Timer1.Stop()
        If b = 0 Then
            quickwin()
            For i = 0 To Val(num - 1)
                For j = 0 To Val(num - 1)
                    If matrixM(i, j).Text = "1" Then
                        plyscore = plyscore + 1
                        Label8.Text = "Your score = " + Str(plyscore)
                    ElseIf matrixM(i, j).Text = "2" Then
                        compscore = compscore + 1
                        Label9.Text = "Computer score = " + Str(compscore)
                    End If
                Next
            Next

            If plyscore + compscore = Val(num) ^ 2 Then
                b = 1
                Label12.Text = ""
                Label1.Enabled = False
                Label2.Enabled = False
                Label3.Enabled = False
                Label4.Enabled = False
                Label5.Enabled = False
                If plyscore > compscore Then
                    MsgBox("Congratulation!", MsgBoxStyle.OkOnly, "You win!")
                ElseIf compscore > plyscore Then
                    MsgBox("Try again later!", MsgBoxStyle.OkOnly, "You lose!")
                ElseIf plyscore = compscore Then
                    MsgBox("It's a draw!", MsgBoxStyle.OkOnly, "Draw")
                End If
            End If

            If b = 0 Then
                If ply2 = plyscore And comp2 = compscore And comp2 > ply2 Then
                    If Label13.Text = "0a" Then
                        MsgBox("Simply change colour doesn't make you win!", MsgBoxStyle.OkOnly, "Accept your fate")
                        For i = 0 To Val(num - 1)
                            For j = 0 To Val(num - 1)
                                If matrixM(i, j).Text = "0" Then
                                    matrixM(i, j).BackColor = Label7.BackColor
                                    matrixM(i, j).Text = "2"
                                End If
                            Next
                        Next
                    ElseIf Label13.Text = "1a" And count = 1 Then
                        MsgBox("Stop choosing colour doesn't change the result!", MsgBoxStyle.OkOnly, "LOL")
                        For i = 0 To Val(num - 1)
                            For j = 0 To Val(num - 1)
                                If matrixM(i, j).Text = "0" Then
                                    matrixM(i, j).BackColor = Label7.BackColor
                                    matrixM(i, j).Text = "2"
                                End If
                            Next
                        Next
                    ElseIf Label13.Text = "1a" Then
                        MsgBox("Stop choosing colour doesn't change the result!", MsgBoxStyle.OkOnly, "LOL")
                        change()
                        count = 1
                    End If
                    checkwin()
                ElseIf ply1 = plyscore And comp1 = compscore Then
                    ply2 = plyscore
                    comp2 = compscore
                Else
                    ply1 = plyscore
                    comp1 = compscore
                End If
            End If
            Timer1.Start()
        End If
    End Sub
    Private Sub quickwin()
      
    End Sub
    Private Sub change()
        Label11.BackColor = Label7.BackColor
        Label7.BackColor = Label6.BackColor
        Label6.BackColor = Label11.BackColor
        player()
        For i = 0 To Val(num - 1)
            For j = 0 To Val(num - 1)
                If matrixM(i, j).Text = "2" Then
                    If i > 0 Then
                        If matrixM(i - 1, j).Text = "0" And matrixM(i - 1, j).BackColor = Label7.BackColor Then
                            matrixM(i - 1, j).Text = "2"
                        End If
                    End If
                    If i < Val(num - 1) Then
                        If matrixM(i + 1, j).Text = "0" And matrixM(i + 1, j).BackColor = Label7.BackColor Then
                            matrixM(i + 1, j).Text = "2"
                        End If
                    End If
                    If j > 0 Then
                        If matrixM(i, j - 1).Text = "0" And matrixM(i, j - 1).BackColor = Label7.BackColor Then
                            matrixM(i, j - 1).Text = "2"
                        End If
                    End If
                    If j < Val(num - 1) Then
                        If matrixM(i, j + 1).Text = "0" And matrixM(i, j + 1).BackColor = Label7.BackColor Then
                            matrixM(i, j + 1).Text = "2"
                        End If
                    End If
                End If
            Next
        Next
        For i = Val(num - 1) To 0 Step -1
            For j = Val(num - 1) To 0 Step -1
                If matrixM(i, j).Text = "2" Then
                    If i > 0 Then
                        If matrixM(i - 1, j).Text = "0" And matrixM(i - 1, j).BackColor = Label7.BackColor Then
                            matrixM(i - 1, j).Text = "2"
                        End If
                    End If
                    If i < Val(num - 1) Then
                        If matrixM(i + 1, j).Text = "0" And matrixM(i + 1, j).BackColor = Label7.BackColor Then
                            matrixM(i + 1, j).Text = "2"
                        End If
                    End If
                    If j > 0 Then
                        If matrixM(i, j - 1).Text = "0" And matrixM(i, j - 1).BackColor = Label7.BackColor Then
                            matrixM(i, j - 1).Text = "2"
                        End If
                    End If
                    If j < Val(num - 1) Then
                        If matrixM(i, j + 1).Text = "0" And matrixM(i, j + 1).BackColor = Label7.BackColor Then
                            matrixM(i, j + 1).Text = "2"
                        End If
                    End If
                End If
            Next
        Next
        For i = 0 To Val(num - 1)
            For j = 0 To Val(num - 1)
                If matrixM(i, j).Text = "2" Then
                    matrixM(i, j).BackColor = Label7.BackColor
                    typeM(i, j) = matrixM(i, j).Text
                    colour()
                End If
            Next
        Next
    End Sub
    Private Sub strategyi()

        For i = 0 To Val(num - 1)
            For j = 0 To Val(num - 1)
                If matrixM(i, j).Text = "0" And checkM(i, j).Text = "0" Then
                    If i > 0 And i < Val(num - 1) Then
                        If matrixM(i - 1, j).Text = "1" And matrixM(i + 1, j).Text = "0" Then
                            checkM(i, j).Text = "1"
                        End If
                    End If
                    If j > 0 And j < Val(num - 1) Then
                        If matrixM(i, j - 1).Text = "1" And matrixM(i, j + 1).Text = "0" Then
                            checkM(i, j).Text = "1"
                        End If
                    End If
                End If
            Next
        Next

        For i = 0 To Val(num - 1)
            For j = 0 To Val(num - 1)
                If checkM(i, j).Text = "1" Then
                    If i > 0 And i < Val(num - 3) Then
                        If matrixM(i - 1, j).Text = "1" And matrixM(i + 2, j).Text = "1" Then
                            checkM(i, j).Text = "-1"
                        End If
                        If matrixM(i - 1, j).Text = "1" And matrixM(i + 3, j).Text = "1" Then
                            checkM(i, j).Text = "-1"
                        End If
                    End If
                    If j > 0 And j < Val(num - 3) Then
                        If matrixM(i, j - 1).Text = "1" And matrixM(i, j + 2).Text = "1" Then
                            checkM(i, j).Text = "-1"
                        End If
                        If matrixM(i, j - 1).Text = "1" And matrixM(i, j + 3).Text = "1" Then
                            checkM(i, j).Text = "-1"
                        End If
                    End If
                End If
            Next
        Next

        For i = 1 To Val(num - 2)
            For j = 1 To Val(num - 2)
                If checkM(i, j).Text = "1" Then
                    If matrixM(i - 1, j - 1).Text = "1" And matrixM(i + 1, j + 1).Text = "1" Or matrixM(i - 1, j + 1).Text = "1" And matrixM(i + 1, j - 1).Text = "1" Then
                        checkM(i, j).Text = "-1"
                    End If
                End If
            Next
        Next

        For j = 1 To Val(num - 2)
            If checkM(0, j).Text = "1" And matrixM(1, j + 1).Text = "1" And matrixM(1, j - 1).Text = "1" Then
                checkM(0, j).Text = "-1"
            End If
            If checkM(num - 1, j).Text = "1" And matrixM(num - 2, j + 1).Text = "1" And matrixM(num - 2, j - 1).Text = "1" Then
                checkM(num - 1, j).Text = "-1"
            End If
        Next
        For i = 1 To Val(num - 2)
            If checkM(i, 0).Text = "1" And matrixM(i + 1, 1).Text = "1" And matrixM(i - 1, 1).Text = "1" Then
                checkM(i, 0).Text = "-1"
            End If
            If checkM(i, num - 1).Text = "1" And matrixM(i + 1, num - 2).Text = "1" And matrixM(i - 1, num - 2).Text = "1" Then
                checkM(i, num - 1).Text = "-1"
            End If
        Next

        For i = 0 To Val(num - 1)
            For j = 0 To Val(num - 1)
                If checkM(i, j).Text = "1" Then
                    If i > 0 Then
                        If checkM(i - 1, j).Text = "0" And matrixM(i - 1, j).BackColor = matrixM(i, j).BackColor And matrixM(i - 1, j).Text = "0" Then
                            checkM(i - 1, j).Text = "1"
                        End If
                    End If
                    If i < Val(num - 1) Then
                        If checkM(i + 1, j).Text = "0" And matrixM(i + 1, j).BackColor = matrixM(i, j).BackColor And matrixM(i + 1, j).Text = "0" Then
                            checkM(i + 1, j).Text = "1"
                        End If
                    End If
                    If j > 0 Then
                        If checkM(i, j - 1).Text = "0" And matrixM(i, j - 1).BackColor = matrixM(i, j).BackColor And matrixM(i, j - 1).Text = "0" Then
                            checkM(i, j - 1).Text = "1"
                        End If
                    End If
                    If j < Val(num - 1) Then
                        If checkM(i, j + 1).Text = "0" And matrixM(i, j + 1).BackColor = matrixM(i, j).BackColor And matrixM(i, j + 1).Text = "0" Then
                            checkM(i, j + 1).Text = "1"
                        End If
                    End If
                End If
            Next
        Next
        For i = Val(num - 1) To 0 Step -1
            For j = Val(num - 1) To 0 Step -1
                If checkM(i, j).Text = "1" Then
                    If i > 0 Then
                        If checkM(i - 1, j).Text = "0" And matrixM(i - 1, j).BackColor = matrixM(i, j).BackColor And matrixM(i - 1, j).Text = "0" Then
                            checkM(i - 1, j).Text = "1"
                        End If
                    End If
                    If i < Val(num - 1) Then
                        If checkM(i + 1, j).Text = "0" And matrixM(i + 1, j).BackColor = matrixM(i, j).BackColor And matrixM(i + 1, j).Text = "0" Then
                            checkM(i + 1, j).Text = "1"
                        End If
                    End If
                    If j > 0 Then
                        If checkM(i, j - 1).Text = "0" And matrixM(i, j - 1).BackColor = matrixM(i, j).BackColor And matrixM(i, j - 1).Text = "0" Then
                            checkM(i, j - 1).Text = "1"
                        End If
                    End If
                    If j < Val(num - 1) Then
                        If checkM(i, j + 1).Text = "0" And matrixM(i, j + 1).BackColor = matrixM(i, j).BackColor And matrixM(i, j + 1).Text = "0" Then
                            checkM(i, j + 1).Text = "1"
                        End If
                    End If
                End If
            Next
        Next

        For i = 0 To Val(num - 1)
            For j = 0 To Val(num - 1)
                If matrixM(i, j).Text = "1" Then
                    If i > 0 Then
                        If matrixM(i - 1, j).Text = "0" Then
                            ignoreM(i - 1, j) = 1
                        End If
                    End If
                    If i < Val(num - 1) Then
                        If matrixM(i + 1, j).Text = "0" Then
                            ignoreM(i + 1, j) = 1
                        End If
                    End If
                    If j > 0 Then
                        If matrixM(i, j - 1).Text = "0" Then
                            ignoreM(i, j - 1) = 1
                        End If
                    End If
                    If j < Val(num - 1) Then
                        If matrixM(i, j + 1).Text = "0" Then
                            ignoreM(i, j + 1) = 1
                        End If
                    End If
                End If
            Next
        Next
        strategyii()
    End Sub
    Private Sub strategyii()
        plabel(0) = Label1
        plabel(1) = Label2
        plabel(2) = Label3
        plabel(3) = Label4
        plabel(4) = Label5
        plabel(5) = Label15
        plabel(0).Text = "0"
        plabel(1).Text = "0"
        plabel(2).Text = "0"
        plabel(3).Text = "0"
        plabel(4).Text = "0"
        plabel(5).Text = "0"
        For i = 0 To Val(num - 1)
            For j = 0 To Val(num - 1)
                If checkM(i, j).Text = "1" Then
                    Dim more As New Byte
                    If i > 0 Then
                        If matrixM(i - 1, j).Text = "0" And ignoreM(i - 1, j) <> 1 Then
                            more = more + 1
                        End If
                    End If
                    If i < Val(num - 1) Then
                        If matrixM(i + 1, j).Text = "0" And ignoreM(i + 1, j) <> 1 Then
                            more = more + 1
                        End If
                    End If
                    If j > 0 Then
                        If matrixM(i, j - 1).Text = "0" And ignoreM(i, j - 1) <> 1 Then
                            more = more + 1
                        End If
                    End If
                    If j < Val(num - 1) Then
                        If matrixM(i, j + 1).Text = "0" And ignoreM(i, j + 1) <> 1 Then
                            more = more + 1
                        End If
                    End If
                    Select Case matrixM(i, j).BackColor
                        Case Is = Label1.BackColor
                            plabel(0).Text = Val(plabel(0).Text) + Val(more)
                        Case Is = Label2.BackColor
                            plabel(1).Text = Val(plabel(1).Text) + Val(more)
                        Case Is = Label3.BackColor
                            plabel(2).Text = Val(plabel(2).Text) + Val(more)
                        Case Is = Label4.BackColor
                            plabel(3).Text = Val(plabel(3).Text) + Val(more)
                        Case Is = Label5.BackColor
                            plabel(4).Text = Val(plabel(4).Text) + Val(more)
                        Case Is = Label15.BackColor
                            plabel(5).Text = Val(plabel(5).Text) + Val(more)
                    End Select
                End If
            Next
        Next

        For n = 0 To 5
            If Val(plabel(n).Text) >= Val(plabel(0).Text) And Val(plabel(n).Text) >= Val(plabel(1).Text) And Val(plabel(n).Text) >= Val(plabel(2).Text) And Val(plabel(n).Text) >= Val(plabel(3).Text) And Val(plabel(n).Text) >= Val(plabel(4).Text) And Val(plabel(n).Text) >= Val(plabel(5).Text) Then
                Label16.BackColor = plabel(n).BackColor
                Label16.Text = plabel(n).Text
            End If
        Next

        Select Case Label16.BackColor
            Case Is = Label1.BackColor
                Label17.Text = clabel(0).Text
                plabel(0).Text = "0"
            Case Is = Label2.BackColor
                Label17.Text = clabel(1).Text
                plabel(1).Text = "0"
            Case Is = Label3.BackColor
                Label17.Text = clabel(2).Text
                plabel(2).Text = "0"
            Case Is = Label4.BackColor
                Label17.Text = clabel(3).Text
                plabel(3).Text = "0"
            Case Is = Label5.BackColor
                Label17.Text = clabel(4).Text
                plabel(4).Text = "0"
            Case Is = Label6.BackColor
                Label17.Text = clabel(5).Text
                plabel(5).Text = "0"
        End Select

        For n = 0 To 5
            If Val(plabel(n).Text) >= Val(plabel(0).Text) And Val(plabel(n).Text) >= Val(plabel(1).Text) And Val(plabel(n).Text) >= Val(plabel(2).Text) And Val(plabel(n).Text) >= Val(plabel(3).Text) And Val(plabel(n).Text) >= Val(plabel(4).Text) And Val(plabel(n).Text) >= Val(plabel(5).Text) Then
                Label18.BackColor = plabel(n).BackColor
                Label18.Text = plabel(n).Text
            End If
        Next
        hold = hold + 1
        If (Val(Label16.Text) - Val(Label7.Text)) > (Val(Label18.Text) - Val(Label17.Text)) And hold = 5 Then
            If Label16.BackColor <> matrixM(num - 1, num - 1).BackColor And Label16.BackColor <> matrixM(0, 0).BackColor Then
                Label7.BackColor = Label16.BackColor
            End If
        End If
        If hold = 2 Then
            Label7.BackColor = matrixM(num - 2, num - 2).BackColor
        End If
    End Sub
    Private Sub strategy2()
        Dim plyboxx, plyboxy, compboxx, compboxy As Byte
        Dim plyboxx1, plyboxy1, compboxx1, compboxy1 As Byte
        Dim appear, save1, save2 As Byte
        appear = 0
        plyboxx = 0
        compboxy = Val(num - 1)
        If num >= 9 Then
            For j = 1 To Floor(Val(num - 1) / 3)
                For i = Floor(Val(num - 1) * 2 / 3) To Val(num - 2)
                    If matrixM(i, j).Text = "1" And plyboxx < i Then
                        plyboxx = i
                        plyboxy = j
                        appear = 1
                    End If
                Next
            Next
            If appear = 1 Then
                For z = num - 1 - plyboxx To 1 Step -1
                    If plyboxy + z - 1 < num - 1 Then
                        If matrixM(plyboxx + z, plyboxy + z - 1).Text = "2" Then
                            compboxx = plyboxx + z
                            compboxy = plyboxy + z - 1
                            save1 = compboxy
                            appear = 2
                        End If
                    End If
                Next
            End If
            If appear = 2 Then
                For z = 1 To save1 - 1
                    If compboxy - z > 0 Then
                        If matrixM(compboxx, compboxy - z).Text = "2" Then
                            compboxy = compboxy - z
                            appear = 3
                        End If
                    End If
                Next
            End If

            plyboxy1 = 0
            compboxx1 = Val(num - 1)
            For j = Floor(Val(num - 1) * 2 / 3) To Val(num - 2)
                For i = 1 To Floor(Val(num - 1) / 3)
                    If matrixM(i, j).Text = "1" And plyboxy1 < j Then
                        plyboxx1 = i
                        plyboxy1 = j
                        appear = 4
                    End If
                Next
            Next
            If appear = 4 Then
                For z = num - 1 - plyboxx1 To 0 Step -1
                    If plyboxy1 + z + 1 < num - 1 Then
                        If matrixM(plyboxx1 + z, plyboxy1 + z + 1).Text = "2" Then
                            compboxx1 = plyboxx1 + z
                            compboxy1 = plyboxy1 + z + 1
                            save2 = compboxy1
                            appear = 5
                        End If
                    End If
                Next
            End If
            If appear = 5 Then
                For z = 1 To save2
                    If compboxx1 - z > 0 Then
                        If matrixM(compboxx1 - z, compboxy1).Text = "2" Then
                            compboxx1 = compboxx1 - z
                            appear = 6
                        End If
                    End If
                Next
            End If


            If appear <> 0 Then
                If compboxx <= compboxy1 And compboxx <> 0 Then
                    If matrixM(compboxx, compboxy - 1).Text = "0" And matrixM(compboxx, compboxy - 1).BackColor <> matrixM(0, 0).BackColor And matrixM(compboxx, compboxy - 1).BackColor <> matrixM(num - 1, num - 1).BackColor Then
                        Label7.BackColor = matrixM(compboxx, compboxy - 1).BackColor
                    ElseIf matrixM(compboxx + 1, compboxy).Text = "0" And matrixM(compboxx + 1, compboxy).BackColor <> matrixM(0, 0).BackColor And matrixM(compboxx + 1, compboxy).BackColor <> matrixM(num - 1, num - 1).BackColor Then
                        Label7.BackColor = matrixM(compboxx + 1, compboxy).BackColor
                    End If
                ElseIf compboxy1 < compboxx And compboxy1 <> 0 Then
                    If matrixM(compboxx1 - 1, compboxy1).Text = "0" And matrixM(compboxx1 - 1, compboxy1).BackColor <> matrixM(0, 0).BackColor And matrixM(compboxx1 - 1, compboxy1).BackColor <> matrixM(num - 1, num - 1).BackColor Then
                        Label7.BackColor = matrixM(compboxx1 - 1, compboxy1).BackColor
                    ElseIf matrixM(compboxx1, compboxy1 + 1).Text = "0" And matrixM(compboxx1, compboxy1 + 1).BackColor <> matrixM(0, 0).BackColor And matrixM(compboxx1, compboxy1 + 1).BackColor <> matrixM(num - 1, num - 1).BackColor Then
                        Label7.BackColor = matrixM(compboxx1, compboxy1 + 1).BackColor
                    End If
                End If
            End If

            For j = Floor(Val(num - 1) * 2 / 3) To Val(num - 2)
                If matrixM(1, j).Text = "2" And matrixM(0, j).Text = "0" And matrixM(0, j).BackColor <> matrixM(0, 0).BackColor And matrixM(0, j).BackColor <> matrixM(num - 1, num - 1).BackColor Then
                    Label7.BackColor = matrixM(0, j).BackColor
                End If
            Next
            For i = Floor(Val(num - 1) * 2 / 3) To Val(num - 2)
                If matrixM(i, 1).Text = "2" And matrixM(i, 0).Text = "0" And matrixM(i, 0).BackColor <> matrixM(0, 0).BackColor And matrixM(i, 0).BackColor <> matrixM(num - 1, num - 1).BackColor Then
                    Label7.BackColor = matrixM(i, 0).BackColor
                End If
            Next
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Label13.Text = "1a" And b = 0 And matrixM(0, 0).BackColor <> Color.White Then
            comp()
        End If
        If Form2.CheckBox4.Checked = True Or b = 1 Then
            Timer1.Stop()
        End If
    End Sub
    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Label10.Text = ""
        Timer2.Stop()
    End Sub

    Private Sub Label12_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label12.TextChanged
        If Form2.CheckBox3.Checked = True Then
            Return
        End If
        If Label12.Text <> "" Then
            If b = 0 Then
                comp()
            End If
        End If
    End Sub
    Private Sub Label13_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label13.TextChanged
        If Label13.Text = "0a" Or Label13.Text = "1a" Then
            reset()
        End If
    End Sub

End Class

