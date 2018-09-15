Public Class Form2

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then
            MsgBox("Please fill in the blank!", MsgBoxStyle.OkOnly)
        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = False Then
            MsgBox("Please choose your mode!", MsgBoxStyle.OkOnly)
        ElseIf CheckBox3.Checked = False And CheckBox4.Checked = False Then
            MsgBox("Please choose your timer mode!", MsgBoxStyle.OkOnly)
        End If
        If Val(TextBox1.Text) > 0 Then
            Form1.Visible = True
            Form1.Enabled = True
            Me.Visible = False
            Form1.Label13.Text = Form1.Label13.Text + "a"
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            CheckBox2.Checked = False
            Form1.Timer1.Interval = 800
        End If
    End Sub

    Private Sub CheckBox4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked = True Then
            CheckBox1.Checked = True
            CheckBox3.Checked = False
            CheckBox2.Checked = False
            Form1.Label13.Text = "0"
            Form1.Label12.Enabled = True
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            CheckBox4.Checked = False
            Form1.Label13.Text = "1"
            Form1.Label12.Enabled = False
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            CheckBox3.Checked = True
            CheckBox1.Checked = False
            CheckBox4.Checked = False
            Form1.Timer1.Interval = 800
            Form1.Label13.Text = "1"
        Else
            CheckBox3.Checked = False
        End If
    End Sub

    Private Sub Form2_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Form1.Enabled = True
    End Sub

    Private Sub Form2_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        If Me.Visible = True Then
            TextBox1.Text = ""
            CheckBox2.Checked = False
            CheckBox3.Checked = False
            CheckBox1.Checked = False
            CheckBox4.Checked = False
            Form1.Enabled = False
            Me.TextBox1.Focus()
        End If
    End Sub

    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class