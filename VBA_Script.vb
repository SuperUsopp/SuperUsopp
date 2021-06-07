Sub get_SFS_ID()
'
' 提取Test Case所链接需求ID
'
'
    Dim index As Integer
    index = 1
    While index <= 220
        Windows("SW").Activate
        With Selection.Find
            .Text = "需求来源/Requirement:"
            .Replacement.Text = ""
            .Forward = True
            .Wrap = wdFindContinue
            .Format = False
            .MatchCase = False
            .MatchWholeWord = False
            .MatchByte = True
            .MatchWildcards = False
            .MatchSoundsLike = False
            .MatchAllWordForms = False
        End With
        
        If index = 1 Then
            Selection.Find.Execute
        Else:
            Selection.Find.Execute
            Selection.Find.Execute
        End If

        Selection.EndKey Unit:=wdLine
        Selection.HomeKey Unit:=wdLine, Extend:=wdExtend
        Selection.Copy
        Windows("SWID").Activate
        'Windows("SW").Activate
        Selection.PasteAndFormat (wdFormatOriginalFormatting)
        Selection.TypeParagraph
        index = index + 1
    Wend
End Sub

Sub Renumber_ID()
'
' 从头重新编号
'
'
    Dim i As Integer
    Selection.HomeKey Unit:=wdStory
    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    i = 1
    While i <= 31
        With Selection.Find
            .Text = "测试用例* /Test Case *:"
            .Replacement.Text = "测试用例" & i & " /Test Case " & i & ":"
            .Forward = True
            .Wrap = wdFindContinue
            .Format = False
            .MatchCase = False
            .MatchWholeWord = False
            .MatchByte = False
            .MatchAllWordForms = False
            .MatchSoundsLike = False
            .MatchWildcards = True
        End With
        Selection.Find.Execute
        Selection.Find.Execute Replace:=wdReplaceOne

        i = i + 1
    Wend
End Sub

Public Sub Delayms(lngTime As Long)
'
'延时函数
'
    Dim StartTime As Single
    Dim CostTime As Single
    StartTime = Timer
     
    Do While (Timer - StartTime) * 1000 < lngTime
        DoEvents
    Loop
     
     
End Sub

Function Copy_and_Replace_Use_ID(submit_id, current_id)
'
'从一个word中copy一段内容到另一段
'
    Windows("Submit.docx").Activate
    
    Rem 激活窗口
    AppActivate "Submit.docx"
    'Delayms (1000)
    'SendKeys "^{A}"
    'Delayms (1000)
    Rem 折叠title
    'SendKeys "+%{-}"
    'Delayms (1000)
    
    With Selection
        .HomeKey Unit:=wdStory, Extend:=wdMove
        Rem todo 搜索内容要替换成ID
        If .Find.Execute(FindText:=submit_id, Forward:=True) Then
            
            Rem 向上移动到Title区域，并将光标移动到开头处
            .MoveUp Unit:=wdLine, Count:=1
            
            Rem 激活窗口
            AppActivate "Submit.docx"
            Delayms (1000)
            Rem 折叠title
            SendKeys "+%{-}"
            Delayms (1000)
            
            .EndKey Unit:=wdLine
            .MoveStart Unit:=wdParagraph, Count:=-1 '选中关键字所在段落
            
            Rem 做一个shift+end按键操作，使得复制全折叠区域
            .EndKey Unit:=wdLine, Extend:=wdExtend
            .Copy
        End If
    End With
    
    Rem test2.doc [兼容模式]要替换成导出文档名
    Windows("Current.docx").Activate
    
    Rem 激活窗口
    AppActivate "Current.docx"
    'Delayms (1000)
    'SendKeys "^{A}"
    'Delayms (1000)
    Rem 折叠title
    'SendKeys "+%{-}"
    'Delayms (1000)
    
    With Selection
        .HomeKey Unit:=wdStory, Extend:=wdMove
            Rem todo 搜索内容要替换成ID
            If .Find.Execute(FindText:=current_id, Forward:=True) Then
            
                Rem 向上移动到Title区域，并将光标移动到开头处
                .MoveUp Unit:=wdLine, Count:=1
                
                Rem 激活窗口
                AppActivate "Current.docx"
                Delayms (1000)
                Rem 折叠title
                SendKeys "+%{-}"
                Delayms (1000)
                
                
                .EndKey Unit:=wdLine
                .MoveStart Unit:=wdParagraph, Count:=-1 '选中关键字所在段落
                
                Rem 做一个shift+end按键操作，使得复制全折叠区域
                .EndKey Unit:=wdLine, Extend:=wdExtend
                
                .PasteAndFormat (wdFormatOriginalFormatting)
                Rem 设置title的格式
                .MoveUp Unit:=wdLine, Count:=1
                .EndKey Unit:=wdLine
                .Style = ActiveDocument.Styles("MyStyle")
                
                .MoveStart Unit:=wdParagraph, Count:=-1 '选中关键字所在段落
                .Shading.BackgroundPatternColor = wdColorYellow
                
                Rem 激活窗口
                AppActivate "Current.docx"
                Delayms (1000)
                Rem 折叠title
                SendKeys "+%{-}"
                Delayms (1000)
            End If
    End With
End Function

Sub Copy_and_Replace_Use_Title()
'
'从一个word中copy一段内容到另一段
'


    Rem test1.doc [兼容模式]要替换成提交文档名
    Windows("Submit.docx").Activate
    
    With Selection
        .HomeKey Unit:=wdStory, Extend:=wdMove
        Rem todo 搜索内容要替换成ID
        If .Find.Execute(FindText:="SIT_开关机_开机_冷启动_平板温度手册检查[SIT_Start up and Shut down_Start up_check the temperation of detector from user manualID: 720882]", Forward:=True) Then
            .MoveStart Unit:=wdParagraph, Count:=-1 '选中关键字所在段落
            '.MoveUp Unit:=wdLine, Count:=1
            '.HomeKey Unit:=wdLine
            
            'AppActivate "test1.doc [兼容模式]"
            Rem 折叠title
            
            'SendKeys "+%{-}"
            
            Rem 重新选择整个段落并复制
            Rem 做一个shift+end按键操作，使得复制全折叠区域
            .EndKey Unit:=wdLine, Extend:=wdExtend
            .Copy
        End If
        
        
        Rem test2.doc [兼容模式]要替换成导出文档名
        Windows("Current.docx").Activate
            With Selection
                .HomeKey Unit:=wdStory, Extend:=wdMove
                    Rem todo 搜索内容要替换成ID
                    If .Find.Execute(FindText:="SIT_开关机_开机_冷启动_平板温度手册检查[SIT_Start up and Shut down_Start up_check the temperation of detector from user manualID: 720882]", Forward:=True) Then
                        .MoveStart Unit:=wdParagraph, Count:=-1 '选中关键字所在段落
                        '.MoveUp Unit:=wdLine, Count:=1
                        '.HomeKey Unit:=wdLine
                        
                        Rem 折叠title
                        'AppActivate "test2.doc [兼容模式]"
                        'SendKeys "+%{-}"
                        Rem 做一个shift+end按键操作，使得复制全折叠区域
                        .EndKey Unit:=wdLine, Extend:=wdExtend
                        .PasteAndFormat (wdFormatOriginalFormatting)
                    End If
        End With
    End With
End Sub

Sub 搜索()
'
' 搜索 宏
'
'
    Dim a, i As Integer
    a = 141
    i = 142
    Application.DisplayAlerts = False
'    With ActiveWindow
'        .Width = 1444
'        .Height = 676
'    End With
    CommandBars("Navigation").Visible = False
    Selection.Find.ClearFormatting
    While i <= 202
    
'       search
        With Selection.Find
            .Text = "测试用例" & i & " /Test Case " & i & ":"
            .Forward = True
            .Wrap = wdFindContinue
            .Format = False
            .MatchCase = False
            .MatchWholeWord = False
            .MatchByte = True
            .MatchWildcards = False
            .MatchSoundsLike = False
            .MatchAllWordForms = False
        End With
        
'        判断search情况
        If (Selection.Find.Execute) Then
            With Selection.Find
            .Text = "测试用例" & i & " /Test Case " & i & ":"
            .Replacement.Text = "测试用例" & a & " /Test Case " & a & ":"
            .Forward = True
            .Wrap = wdFindAsk
            .Format = False
            .MatchCase = False
            .MatchWholeWord = False
            .MatchByte = True
            .MatchWildcards = False
            .MatchSoundsLike = False
            .MatchAllWordForms = False
             End With
            With Selection
            If .Find.Forward = True Then
                .Collapse Direction:=wdCollapseStart
            Else
                .Collapse Direction:=wdCollapseEnd
            End If
            .Find.Execute Replace:=wdReplaceOne
            If .Find.Forward = True Then
                .Collapse Direction:=wdCollapseEnd
            Else
                .Collapse Direction:=wdCollapseStart
            End If
            .Find.Execute
            End With
            a = a + 1
            i = i + 1
        Else
            i = i + 1
        End If
    Wend
    Application.DisplayAlerts = True
End Sub