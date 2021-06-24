'Private Declare PtrSafe Sub sleepp Lib "kernel32.dll" Alias "Sleep" (ByVal dwMilliseconds As LongLong)
'Private Declare PtrSafe Function DeleteUrlCacheEntry Lib "wininet" Alias "DeleteUrlCacheEntryA" (ByVal lpszUrlName As String) As LongLong

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

Function Copy_and_Replace_Use_ID(submit_ID, current_id)
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
        If .Find.Execute(FindText:=submit_ID, Forward:=True) Then
            
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

Sub Run()
'
'调用Copy_and_Replace_Use_ID
'
Application.DisplayAlerts = False
Dim index As Integer

Dim all_relation_sys() As Variant
Dim all_relation_pra() As Variant
Dim all_relation_sw() As Variant
Dim all_relation_iq() As Variant

'Total:186
all_relation_sys = Array("720981-913178", "720993-913108", "720982-913151", "720988-913114", "720987-913149", "720995-913177", "720989-913070", "721025-913121", "865923-913293", "865954-913295", "721068-913125", "720851-913286", "720830-913271", "721035-913307", "720967-913302", "720966-913301", "720960-913195", "720962-913350", "720959-913183", "720956-913167", "720957-913131", "720958-913132", "721033-913305", "720856-913290", "720846-913281", "720847-913282", "720863-913258", "720860-913373", "720859-913372", "720858-913370", "720844-913280", "720849-913284", "720888-913292", "720895-913364", _
"720897-913366", "720891-913360", "720893-913362", "720898-913367", "720630-913331", "720890-913359", "720892-913361", "720901-913332", "720909-913339", "720905-913335", "720908-913338", "720906-913336", "720907-913337", "720904-913334", "720896-913365", "721050-913319", "720964-913299", "721059-913326", "721045-913347", "721054-913345", "721046-913308", "721049-913348", "721058-913325", "721026-913122", "721011-913237", "721019-913236", "721044-913346", "721036-913349", "721037-913311", "721057-913324", "721056-913344", "720999-913143", "721101-913298", "721084-913169", _
"721100-913297", "721085-913170", "721086-913171", "721087-913230", "721083-913221", "721094-913270", "721095-913262", "721089-913213", "721091-913215", "721093-913165", "721081-913189", "721098-913352", "721092-913216", "721096-913268", "721021-913266", "720968-913369", "720848-913283", "720852-913287", "720861-913273", "720832-913156", "720939-913193", "720941-913185", "720940-913140", "720942-913201", "720943-913127", "720938-913354", "721097-913244", "720867-913076", "720868-913077", "720870-913092", "720871-913155", "720887-913079", "720885-913159", "720881-913107", _
"720886-913160", "720880-913106", "720882-913071", "720872-913098", "720877-913103", "720874-913100", "720876-913102", "720884-913078", "720878-913104", "720873-913099", "720875-913101", "720883-913161", "720889-913072", "720894-913363", "721022-913118", "721023-913119", "720916-913097", "720912-913220", "720917-913154", "720914-913094", "721024-913120", "720945-913202", "720920-913197", "720949-913197", "720921-913198", "720946-913198", "720922-913199", "720923-913225", "720951-913226", "720924-913200", "720934-913206", "720918-913196", "720926-913353", "720952-913194", _
"720927-913209", "720935-913207", "720928-913210", "720950-913192", "720929-913224", "720930-913211", "720937-913208", "720931-913212", "720932-913126", "720933-913205", "720971-913219", "720994-913186", "720970-913179", "720980-913115", "720973-913069", "721061-913124", "721032-913123", "720974-913110", "720991-913180", "720975-913134", "721067-913191", "721078-913187", "721072-913355", "721073-913356", "721077-913174", "721070-913218", "721076-913190", "721069-913217", "721079-913188", "720814-913082", "720822-913087", "720823-913157", "720828-913090", "720813-913081", _
"720818-913152", "720862-913274", "720811-913162", "720824-913175", "720810-913075", "720806-913074", "720807-913164", "720821-913086", "720815-913083", "720816-913158", "720825-913327", "720820-913085", "720826-913088", "720829-913091", "720819-913084", "720869-913357")

'Total:64
all_relation_pra = Array("49211-911942", "720515-911890", "837186-911956", "837188-911957", "720539-911822", "720553-911895", "837213-911964", "849207-911941", "837193-911958", "837203-911959", "837211-911963", "837204-911960", "837219-911966", "837215-911965", "849240-911945", "837221-911967", "837209-911962", "837208-911961", "720536-911879", "720546-911849", "720510-911904", "720575-911866", "720639-911846", "720585-911858", "720522-911888", "720635-911831", "720507-911810", "720534-911820", "720547-911852", "720551-911807", "837092-911943", "720625-911910", "720542-911824", "720518-911894", _
"720509-911892", "720560-911875", "720615-911973", "720612-911971", "720527-911872", "720508-911891", "720565-911869", "720614-911937", "850663-911948", "720605-911906", "720608-911924", "720552-911901", "720541-911877", "720622-911913", "720626-911911", "720594-911837", "720636-911899", "720544-911896", "720550-911873", "720530-911882", "720532-911884", "720554-911934", "720545-911903", "720561-911902", "720517-911905", "720641-911898", "720521-911887", "720623-911938", "720549-911897", "720588-911930")

'Total:152
all_relation_sw = Array("78236-913444", "78284-913445", "77719-913440", "83828-913454", "78551-913446", "226166-913575", "79665-913449", "122358-913607", "645800-913518", "645637-913491", "646110-913538", "645743-913503", "645778-913510", "645792-913514", "645725-913499", "645973-913529", "645700-913497", "645260-913462", "646097-913537", "645838-913522", "646025-913533", "646028-913534", "645774-913508", "645798-913517", "646010-913531", "645764-913505", "646653-913558", "645580-913487", "645586-913488", "645720-913498", "645319-913471", "645475-913480", "645663-913494", "645836-913521", _
"645736-913502", "645776-913509", "645757-913504", "645797-913515", "645328-913473", "645769-913507", "645303-913468", "645789-913512", "645282-913464", "645768-913506", "646079-913535", "646577-913548", "646576-913547", "646580-913549", "646583-913551", "646621-913553", "645981-913530", "646090-913536", "646021-913532", "646618-913552", "645619-913524", "645847-913524", "645784-913511", "645302-913467", "645510-913523", "645843-913523", "645626-913490", "645547-913486", "645283-913465", "645856-913528", "645338-913475", "645355-913478", "645345-913476", "645316-913470", _
"645426-913479", "645321-913527", "645849-913527", "645332-913474", "645522-913483", "646116-913539", "645646-913493", "645284-913466", "645504-913481", "645276-913463", "646641-913557", "646631-913555", "646636-913556", "646629-913554", "646550-913544", "646560-913545", "646572-913546", "645527-913484", "645642-913492", "645533-913485", "645829-913519", "645255-913461", "645733-913501", "645834-913520", "645685-913496", "646184-913541", "645677-913495", "645791-913513", "646177-913540", "645351-913477", "646187-913542", "645728-913500", "645192-913458", "645253-913460", _
"645230-913459", "645313-913469", "702879-913559", "451101-913579", "451087-913578", "702798-913582", "702794-913580", "703005-913586", "703305-913587", "725272-913588", "703000-913585", "702840-913584", "702801-913583", "703010-913561", "702723-913435", "725256-913698", "127662-913608", "80940-913451", "80334-913450", "77446-913595", "76453-913390", "89557-913393", "75775-913658", "119647-913597", "121039-913603", "69257-913677", "69255-913676", "69405-913678", "67816-913591", "120992-913602", "69194-913673", "69229-913675", "67685-913590", "69879-69879", "77207-913409", _
"77768-913442", "77761-913441", "77250-913411", "168923-913455", "77210-913410", "77461-913439", "77454-913438", "197922-913456", "77439-913686", "77271-913696", "122268-913606", "312970-913576", "313028-913577", "77218-913416", "197955-913695")

'Total:6
all_relation_iq = Array("808539-808539", "808551-808551", "808554-808554", "808559-808559", "808532-808532", "808561-808561")

index = 0
While index <= 185
    a = Split(all_relation_sys(index), "-")
    submit = a(0)
    current = a(1)
    'Result = Copy_and_Replace_Use_ID(submit, current)
    Result = del_Specified_TestCase(submit)
    index = index + 1
Wend
jieguo = MsgBox("ok", vbOKOnly, "Result")
Application.DisplayAlerts = True
End Sub


Sub BKP()
'
'调用Copy_and_Replace_Use_ID
'
Dim all_id_corresponding_relation() As Variant
all_id_corresponding_relation = Array("78236-913444", "78284-913445", "77719-913440", "83828-913454", "78551-913446", "720981-913178", "720993-913108", "720982-913151", "720988-913114", "720987-913149", "720995-913177", "720989-913070", "226166-913575", "79665-913449", "849211-911942", "720515-911890", "837186-911956", "837188-911957", "720539-911822", "720553-911895", "837213-911964", "849207-911941", "837193-911958", "837203-911959", "837211-911963", "837204-911960", "837219-911966", "837215-911965", "849240-911945", "837221-911967", "837209-911962", "837208-911961", "720536-911879", "720546-911849", "720510-911904", "720575-911866", "720639-911846", "720585-911858", "720522-911888", "720635-911831", "720507-911810", "720534-911820", "720547-911852", "720551-911807", "837092-911943", "720625-911910", "720542-911824", "720518-911894", "720509-911892", "720560-911875", "720615-911973", "720612-911971", "720527-911872", "720508-911891", "720565-911869", _
"720614-911937", "850663-911948", "720605-911906", "720608-911924", "720552-911901", "720541-911877", "720622-911913", "720626-911911", "720594-911837", "720636-911899", "720544-911896", "720550-911873", "720530-911882", "720532-911884", "720554-911934", "720545-911903", "720561-911902", "720517-911905", "720641-911898", "720521-911887", "720623-911938", "720549-911897", "720588-911930", "122358-913607", "645800-913518", "645637-913491", "646110-913538", "645743-913503", "645778-913510", "645792-913514", "645725-913499", "645973-913529", "645700-913497", "645260-913462", "646097-913537", "645838-913522", "646025-913533", "646028-913534", "645774-913508", "645798-913517", "646010-913531", "645764-913505", "646653-913558", "645580-913487", "645586-913488", "645720-913498", "645319-913471", "645475-913480", "645663-913494", "645836-913521", "645736-913502", "645776-913509", "645757-913504", "645797-913515", "645328-913473", "645769-913507", "645303-913468", "645789-913512", "645282-913464", "645768-913506", _
"646079-913535", "646577-913548", "646576-913547", "646580-913549", "646583-913551", "646621-913553", "645981-913530", "646090-913536", "646021-913532", "646618-913552", "645619-913524", "645847-913524", "645784-913511", "645302-913467", "645510-913523", "645843-913523", "645626-913490", "645547-913486", "645283-913465", "645856-913528", "645338-913475", "645355-913478", "645345-913476", "645316-913470", "645426-913479", "645321-913527", "645849-913527", "645332-913474", "645522-913483", "646116-913539", "645646-913493", "645284-913466", "645504-913481", "645276-913463", "646641-913557", "646631-913555", "646636-913556", "646629-913554", "646550-913544", "646560-913545", "646572-913546", "645527-913484", "645642-913492", "645533-913485", "645829-913519", "645255-913461", "645733-913501", "645834-913520", "645685-913496", "646184-913541", "645677-913495", "645791-913513", "646177-913540", "645351-913477", "646187-913542", "645728-913500", "645192-913458", "645253-913460", "645230-913459", "645313-913469", _
"721025-913121", "702879-913559", "451101-913579", "451087-913578", "702798-913582", "702794-913580", "703005-913586", "703305-913587", "725272-913588", "703000-913585", "702840-913584", "702801-913583", "703010-913561", "702723-913435", "725256-913698", "127662-913608", "865923-913293", "865954-913295", "721068-913125", "720851-913286", "720830-913271", "721035-913307", "720967-913302", "720966-913301", "720960-913195", "720962-913350", "720959-913183", "720956-913167", "720957-913131", "720958-913132", "721033-913305", "720856-913290", "720846-913281", "720847-913282", "720863-913258", "720860-913373", "720859-913372", "720858-913370", "720844-913280", "720849-913284", "720888-913292", "720895-913364", "720897-913366", "720891-913360", "720893-913362", "720898-913367", "720630-913331", "720890-913359", "720892-913361", "720901-913332", "720909-913339", "720905-913335", "720908-913338", "720906-913336", "720907-913337", "720904-913334", "720896-913365", "721050-913319", "720964-913299", "721059-913326", _
"721045-913347", "721054-913345", "721046-913308", "721049-913348", "721058-913325", "721026-913122", "721011-913237", "721019-913236", "721044-913346", "721036-913349", "721037-913311", "721057-913324", "721056-913344", "720999-913143", "721101-913298", "721084-913169", "721100-913297", "721085-913170", "721086-913171", "721087-913230", "721083-913221", "721094-913270", "721095-913262", "721089-913213", "721091-913215", "721093-913165", "721081-913189", "721098-913352", "721092-913216", "721096-913268", "721021-913266", "720968-913369", "720848-913283", "720852-913287", "720861-913273", "720832-913156", "720939-913193", "720941-913185", "720940-913140", "720942-913201", "720943-913127", "720938-913354", "721097-913244", "80940-913451", "80334-913450", "720867-913076", "720868-913077", "720870-913092", "720871-913155", "77446-913595", "720887-913079", "720885-913159", "720881-913107", "720886-913160", "720880-913106", "720882-913071", "720872-913098", "720877-913103", "720874-913100", "720876-913102", _
"720884-913078", "720878-913104", "720873-913099", "720875-913101", "720883-913161", "720889-913072", "76453-913390", "89557-913393", "75775-913658", "720894-913363", "119647-913597", "121039-913603", "69257-913677", "69255-913676", "69405-913678", "67816-913591", "120992-913602", "69194-913673", "69229-913675", "67685-913590", "69879-69879", "721022-913118", "721023-913119", "77207-913409", "77768-913442", "77761-913441", "77250-913411", "168923-913455", "77210-913410", "77461-913439", "77454-913438", "197922-913456", "77439-913686", "77271-913696", "720916-913097", "720912-913220", "720917-913154", "122268-913606", "720914-913094", "721024-913120", "720945-913202", "720920-913197", "720949-913197", "720921-913198", "720946-913198", "720922-913199", "720923-913225", "720951-913226", "720924-913200", "720934-913206", "720918-913196", "720926-913353", "720952-913194", "720927-913209", "720935-913207", "720928-913210", "720950-913192", "720929-913224", "720930-913211", "720937-913208", _
"720931-913212", "720932-913126", "720933-913205", "720971-913219", "720994-913186", "720970-913179", "720980-913115", "720973-913069", "721061-913124", "721032-913123", "720974-913110", "720991-913180", "720975-913134", "721067-913191", "312970-913576", "313028-913577", "721078-913187", "721072-913355", "721073-913356", "721077-913174", "721070-913218", "721076-913190", "721069-913217", "721079-913188", "77218-913416", "720814-913082", "720822-913087", "720823-913157", "720828-913090", "720813-913081", "720818-913152", "720862-913274", "720811-913162", "720824-913175", "720810-913075", "720806-913074", "720807-913164", "720821-913086", "720815-913083", "720816-913158", "720825-913327", "720820-913085", "720826-913088", "720829-913091", "720819-913084", "720869-913357")



tmp = "720868-913114"
a = Split(tmp, "-")
submit = a(0)
current = a(1)
Result = Copy_and_Replace_Use_ID(submit, current)
End Sub

Sub Renumber_ID()
'
' 从头重新编号 宏
'
'
    Dim i As Integer
    Selection.HomeKey Unit:=wdStory
    Selection.Find.ClearFormatting
    Selection.Find.Replacement.ClearFormatting
    i = 1
    While i <= 275
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

Sub get_SFS_ID_From_ITS()
'
' 提取Test Case所链接SFS ID
'
'
    Application.DisplayAlerts = False
    Dim index As Integer
    index = 1
    While index <= 199
        Windows("PRA_ITS").Activate
        With Selection.Find
            '.Text = "需求来源/Requirement:"
            .Text = "测试方法和步骤/ Test Method and Procedure:"
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

        Selection.MoveUp Unit:=wdLine, Count:=1
        Selection.EndKey Unit:=wdLine
        'Selection.HomeKey Unit:=wdLine, Extend:=wdExtend
        Selection.MoveStart Unit:=wdParagraph, Count:=-1
        
        Selection.Copy
        Windows("PRA_ITS_ID").Activate
        'Windows("SW").Activate
        Selection.PasteAndFormat (wdFormatOriginalFormatting)
        Selection.TypeParagraph
        index = index + 1
    Wend
    Result = MsgBox("ok", vbOKOnly, "Result")
    Application.DisplayAlerts = True
End Sub

Function del_Specified_TestCase(submit_ID)
'
' 删除指定的TestCase
'
'
    Windows("Submit.docx").Activate
    'AppActivate "Submit.docx"
    
    Selection.Find.ClearFormatting
    With Selection.Find
        .Text = submit_ID
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
    Selection.Find.Execute
    
    Rem 向上移动到Title区域，并将光标移动到开头处
    Selection.MoveUp Unit:=wdLine, Count:=1
    
    Rem 激活窗口
    'AppActivate "Submit.docx"
    'Delayms (1000)
    Rem 折叠title
    SendKeys "+%{-}"
    Delayms (1000)
    
    Selection.EndKey Unit:=wdLine
    Selection.MoveStart Unit:=wdParagraph, Count:=-1 '选中关键字所在段落
    
    Rem 做一个shift+end按键操作，使得复制全折叠区域
    Selection.EndKey Unit:=wdLine, Extend:=wdExtend
    
    'Selection.MoveUp Unit:=wdLine, Count:=1
    'Selection.HomeKey Unit:=wdLine
    'Selection.EndKey Unit:=wdLine, Extend:=wdExtend
    Selection.Delete Unit:=wdCharacter, Count:=1
End Function
Sub get_SFS_ID_From_SFS()
'
' 从SFS中提取需求ID
'
'
    Application.DisplayAlerts = False
    Dim index As Integer
    Dim total_sfs As Integer
    
    index = 1
    total_sfs = 389
    
    While index <= total_sfs
        Windows("SFS-SYS-0616").Activate
        Selection.Find.ClearFormatting
        'Selection.Find.Replacement.ClearFormatting
        With Selection.Find
            .Text = "ID: ^#"
            '.Text = "HaiDi DS:"
            '.Text = "DPS:"
            '.Replacement.Text = ""
            '.Forward = True
            '.Wrap = wdFindContinue
            '.Format = False
            '.MatchCase = False
            '.MatchWholeWord = False
            '.MatchByte = True
            '.MatchWildcards = False
            '.MatchSoundsLike = False
            '.MatchAllWordForms = False
        End With
        
        If index = 1 Then
            Selection.Find.Execute
        Else:
            Selection.Find.Execute
            Selection.Find.Execute
        End If
            
        Selection.EndKey Unit:=wdLine, Extend:=wdExtend
        Selection.Copy
        
        '打开新文档，存进去
        Windows("ID_0616").Activate
        Selection.PasteAndFormat (wdPasteDefault)
        
        index = index + 1
    Wend
    
    Result = MsgBox("ok", vbOKOnly, "Result")
    Application.DisplayAlerts = True
    
End Sub
Sub ID()
'
' ID 宏
'
'
    Selection.Find.ClearFormatting
    With Selection.Find
        .Text = "ID:"
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
    Selection.Find.Execute
    Selection.MoveRight Unit:=wdCharacter, Count:=1
    Selection.EndKey Unit:=wdLine, Extend:=wdExtend
    Selection.Copy
End Sub
Sub AddFormatForSubmitSFSKey()
'
' AddFormatForSubmitSFSKey 宏
'
'
    Application.DisplayAlerts = False
    Dim index As Integer
    Dim total As Integer
    Dim all_relation_pra_submit() As Variant
    Dim all_relation_pra_left() As Variant
    
    index = 0
    total = 89
    
    all_relation_pra_submit = Array("63615", "63616", "63617", "63622", "63632", "63636", "63639", "63640", "63641", "63642", "63643", "63648", "63649", "63652", "63653", "63667", "63668", "63672", "584039", "63674", "63676", "63675", "63679", "63698", "63703", "118693", "63705", "63712", "63715", "63716", "63722", "63734", "63737", _
    "63738", "63740", "63744", "63767", "164392", "63805", "63806", "63807", "63845", "75233", "75236", "75239", "75243", "77659", "118206", "118207", "118212", "118215", "118223", "118224", "118689", "202755", "63650", "221712", "416846", "479471", "498343", "531354", "531365", "531368", "531372", "628346", _
    "545427", "558365", "558385", "558393", "559087", "591321", "591323", "635312", "694326", "694330", "694337", "694356", "694355", "694407", "694491", "694563", "694572", "694586", "694607", "785888", "804343", "804353", "807337", "815453", "806652", "806746", "806759", "806780", "806817", "806833", _
    "806838", "806843", "806846", "806859", "806893", "806933", "806935", "806958", "815440")
    
    all_relation_pra_left = Array("63618", "77657", "63619", "63621", "415415", "63625", "63630", "63631", "63633", "63644", "63646", "63654", "63655", "118690", "119299", "496939", "63669", "63673", "63678", "415214", "63684", "63689", "63685", "63699", "63701", "63702", "63713", "63721", "63735", "63731", "63746", "63747", "415222", "415224", "63760", _
    "63761", "63771", "63773", "63774", "63777", "63778", "63780", "63781", "63783", "63784", "63787", "63788", "63792", "63793", "63794", "63804", "63808", "63809", "63816", "63817", "63825", "63826", "415403", "63832", "63839", "63840", "63842", "63834", "118228", "118191", "63846", "75240", "75244", "515865", "118194", _
    "596032", "118213", "118231", "545441", "641083", "411252", "430342", "430497", "694463", "711259", "543579", "479727", "479731", "531356", "531326", "626779", "694495", "694565", "694612", "806836")
    
    
    While index <= total
                
        Selection.Find.ClearFormatting
        Selection.Find.Replacement.ClearFormatting
        
        With Selection.Find.Replacement.Font
            .Bold = True
            .Color = 2222222
        End With
        
        
        With Selection.Find
            .Text = all_relation_pra_left(index)
            .Replacement.Text = all_relation_pra_left(index)
            .Forward = True
            .Wrap = wdFindContinue
            .Format = True
            .MatchCase = False
            .MatchWholeWord = False
            .MatchByte = True
            .MatchWildcards = False
            .MatchSoundsLike = False
            .MatchAllWordForms = False
        End With
        Selection.Find.Execute Replace:=wdReplaceAll
                
        index = index + 1
    Wend
    
    Result = MsgBox("ok", vbOKOnly, "Result")
    Application.DisplayAlerts = True
End Sub
Sub AddDelLine()
    Application.DisplayAlerts = False
    Dim index As Integer
    Dim total As Integer
    Dim all_relation_pra() As Variant
    
    index = 0
    total = 146
    
    all_relation_pra = Array("63623", "63626", "63629", "63634", "63637", "63638", "63645", "63663", "63680", "63681", "63688", "63692", "63697", "63700", "63714", "63732", "63733", "63736", "63742", "63743", "63752", "63753", "63754", "63755", "63756", "63759", "63762", "63763", "63764", "63765", "63766", "63768", "63770", "63772", "63782", "63785", _
    "63789", "63790", "63795", "63799", "63801", "63802", "63803", "63810", "63811", "63812", "63813", "63814", "63815", "63822", "63823", "63824", "63827", "63829", "63841", "63844", "63847", "75227", "75229", "75230", "75232", "75234", "75238", "75241", "75242", "75245", "75246", "75247", "118189", "118190", "118192", _
    "118193", "118195", "118196", "118197", "118198", "118199", "118200", "118201", "118202", "118205", "118208", "118209", "118210", "118211", "118214", "118216", "118217", "118218", "118221", "118222", "118232", "118233", "118234", "118235", "118236", "118691", "118694", "118695", "120118", "178709", "178710", "225746", _
    "415206", "415226", "415232", "415234", "415397", "415401", "415405", "468711", "468714", "468719", "479481", "479517", "479520", "515661", "515670", "515685", "515716", "595929", "596312", "626453", "630516", "641165", "694367", "694376", "694507", "694513", "694524", "694538", "694539", "711247", "711327", "772535", _
    "415229", "430501", "63647", "63682", "63683", "63690", "63691", "63731", "63739", "63749", "63776", "63828")
    
    While index <= total
        Selection.Find.ClearFormatting
        Selection.Find.Replacement.ClearFormatting
        
        With Selection.Find.Replacement.Font
            .Bold = True
            .StrikeThrough = True
            .DoubleStrikeThrough = False
            .Hidden = False
            .SmallCaps = False
            .AllCaps = False
            .Color = wdColorRed
            .Superscript = False
            .Subscript = False
        End With
        
        With Selection.Find
            .Text = all_relation_pra(index)
            .Replacement.Text = all_relation_pra(index)
            .Forward = True
            .Wrap = wdFindContinue
            .Format = True
            .MatchCase = False
            .MatchWholeWord = False
            .MatchByte = True
            .MatchWildcards = False
            .MatchSoundsLike = False
            .MatchAllWordForms = False
        End With
        Selection.Find.Execute Replace:=wdReplaceAll
        index = index + 1
    Wend
    
    Result = MsgBox("ok", vbOKOnly, "Result")
    Application.DisplayAlerts = True
End Sub
