﻿ALTER TABLE [dbo].[Goods] ADD [IsWebDownloaded] [bit] NOT NULL DEFAULT 0
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'202002091735099_3', N'Mouser.Domain.Migrations.Configuration',  0x1F8B0800000000000400ED1D5D6FE3B8F1BD40FF83E0A7B6D88B93DDA26883E40E5967F71AF492F8E2E4DAB70523318E70B2E4D3C79E83A2BFAC0FFD49FD0B252559E2F797245B4E83058235450E8733C32139E4CCFCF7DFFF39FB6EB38ABCAF30CDC2243E9F9C1C1D4F3C18FB4910C6CBF349913F7DF3E7C977DFFEF637679F82D5C6FB695BEF03AE875AC6D9F9E439CFD7A7D369E63FC315C88E56A19F2659F2941FF9C96A0A8264FAFEF8F82FD39393294420260896E79DDD15711EAE60F903FD9C25B10FD77901A2EB2480515697A32F8B12AA770356305B031F9E4FAE932283E9D165B202613CF12EA210202416307A9A78208E931CE408C5D3870C2EF23489978B352A00D1FDCB1AA27A4F20CA608DFA695BDD7414C7EFF128A66DC32D28BFC87284911DC0930F3559A66C7327E24E1AB221C27D4204CE5FF0A84BE29D4F2EA21CA631C8E11CF83F8325E2EFC463BB3D9D45296EC250F9A80416C2EC8807F2CEA3AABE6B64048912FEF7CE9B15515EA4F03C86459E82E89D372F1EA3D0FF1B7CB94F7E86F1795C4411893A421E7DA30A50D13C4DD630CD5FEEE0533DA0AB60E24DE97653B661D38C68530DF02ACE3FBC9F7837A873F018C1463208622CF22485DFC318A668C0C11CE478EC18062C69CBF5CEF47531BF7E4AE737DB0E91349634BF069B1F60BCCC9F1191C166E27D0E3730D896D4483CC4219A82A8519E1690EBE7067C0D97258A4C8FDF27091ADF1D8CCA8FD973B8AE2686806D5FAABA9FD36475974442F128AB7C592445EA231CEE1375BD7B902E614EE37A366DA550299B1532D6D2889BBDC99FACAF4B9083C53384F9431AF52F846C6730F3D3705DE9AE81FBBA5A812512BFFC79F09EAE415C3C011F8B4F3A07697E53AC1E61AAE8F6E4F8B88F6EC35827261A0048E43B422827D50EC78C7E06859F5FC21C84D12E24F60EC2A8845CF5F33141EA0DC4D684BABBFDEB6281266C910D8EF1A2582E618654C11D5C47682FB4427A60F8298044295C84AB8F11AEDB45A4AEB2BFC3C7877580955957AA5F651F8BECA53B1484D165F26B1C252070404ABA10F38B64D6F7BABC5D6F75EBF276FD36457D86402C93F445886F09B1ADD162497DE0F60CF457D14E418511A984E558D1B518CCC88F62ECA81AB618CED3D0871F53087E16B3B9FDCEB197F9C4B195FD6ECBCE5AAD5EE4483F3C1639942148D712A029A820405654CB11E559B25AA3B31D3A182A716EABC990666AC8B066AB89D036DED0B673C47A53BB6DFAB6B195F585FF0EBEEEA1AD0FC2463F2CDD0AD34A425F8B8B561F369A56A613851538BD28AED5E99C4723643D35C8E66FD363AFD303FFBD985FEDA69F6807DBDC52981ED270473D5D5D1202F1A73F3A2896870C76DDFF5647BBDBA73B98753E30A233509A238ADD413F493B6ACD0504A9FF7C0F3703F05DB7F10D25DB0D47A5CA6E36D4AAD7C6D02746D3722FCCA227DF2D3BE9FB76F7EAA0EDDBC66FBA5ED6D78F05A8EB75996F25A507577CB322455B2AFF6587735A6A10B73891B1DB22D989CD7182D02726A7694283789B2CB2BE1A12ED6487D4F4F613204C67FB157AA7533E3F0154B6802ED3A03D82BBCF8316C6DB4490F5D5D2682733A1ED6E6C53C1DE7824990C321B93D36CB85887777079153F252E37F44DE337F997F585AFB550DBB6CBC1441F1DD2D21D99ABB2EC57E2E03558479F902C0D7F3977955DF879F8D5F2846B33C3AA33E60266597965ED32CF28106FB34DD6D725C81B3EE2FFDF87783EECE5C26F9614718EED1DBF1430CBB36EA7A66BE03F87F16E96D05D3EAF98A1B34D0EDDB826BF1125D634D14528339BBE90F589CB507935FEA192A26EEF57905C6732138CB2A27E0C1DAF29938DF85E97EBA7AEAAC0BBACA147B8AAD6F570B071B9482BDBBDA965695FF38B2048119B86DF9B246947E3B2DB76E0F56DC78CE7CC8F052C5C0ED165BBB7392367EB12DE245D65D96927E3BE36953C952E48FC574EA90BAAD828F48B2C4BFCB0C48A791D4123458FF7531C7846B7D5D5FC22AF68D034C38FD6D081DC47E89C4FFEC0115307BCB9166981D3F4A33B3899B0A27D1B5FC208E6D0C37A0B7B03CC40E68380672BA25940D190A0969A88F45B2CD9F8240FB3DA7155760F738289DF721970E1F8E8E8841518BBC11A498BE2BD57C7411F9C84B0971CB2D1496F3CDAB1911779E664933E6E5373624FC41219C7E523535ACA49C2B1173C36E4533EBA1B2F1159B3AA6680521B2B4746F282C09A8ED277802324A4EC61B06C885AEF9D768C223F30735A6A9F238F91980AE382749C26960682A89C41D182A406860ABAABC632D2E30AABB66018E3AE5B797B25549715B95F6255360A63C419E3C940E4A16D2FB436DDF4BB3F139C316468AA0E1C2D8AF501D67CF48A434AFF12511D7466499CA3F3314C1BA9C0B0CACAF853F97C8D3B813F64B03E8467F5099F1D1106BD80B9CA95A43D6829943A472A1A70FD868C0355A96D4D63F2B11C07A13D0168A090DC1001A2B9A50146396170A0C80DAC1610E7322100C76EEBCC80524E0D32A8E42E4703B65D0C8452412C157A4094F290806394937ED01BB188D41A48D3BE9CD3A2E6B576609A1393959354F6DD265157F9C093DDAE989A259AA1D073656A0B6FABC30878CCAC61B74934150C28C43875F19451581A0C6C0D04E6B5CA5110416C5D3022A6EBC87572A1313D181A1FECA9B06B31E09E44F2A4501A148C4C0A04FE94BE5610436644D051D48904C207722242688D0516E6028A28DCDAA3248DCA40301C81B86753520A292D0136B6009E46D452AA2792ECF43F0095A45EBD3C998CCEF956277D623CC2ADA38252BAB3FD10A452DDFB0BC8657A92B73ECB9364E3B7422AA2199CDE69E0ED86AD7FF2E916338B53BEC339BF3F22EE7AF1933D8030A0A0E0E86F73F8EF8F66F4719F56989B7E364CA2AB449E443A6380A9398018C2F624A0A087C200D083E06C2F379B337FF3ED6C5AC528AB0BCEA692606667D760BDC63AB86D5997788B2AB2D9EC9B857DDCAF550563EA6782F05F0DB64D4F7992822564BEE2877101FC1CA6598E83063D027CFF3C0B565C358185437282DB76283762F09CDC1EF2B66DF1FF499FC9ED6B0581B18301D6D2F6331A2E8E93528E1C2A17471E8887E3CF8108A4822707B3242A56B1ECD982AA7513178C04D114F270CEA6CC5858CA4D39D27196739A2B463CAB9678772E89F631067C11371B861374842C120EFDC50222F94E9302487E30874784BAA286D9169BC39205B32201CBEA58F41232232F0B2CDA979EC81480B2C4020217BA8A82C67D35873CE762549190F9AFE6909B605424C0A6D0020E118D8A0245949B4313079C22E18A6BD871BB8D2EC572BDFD623163A82853D4A4A1BED840AC1E6CD1B0AA324BBCC858531C6AE4C7D1AC0284BDCD7D2990D90E0D960379D3619684EA7528D9BE2AB150124DE0184A3934A53622D35A65696991DDB0EC5150E88DB5BBACA88E0D06F2A26E3E5699C17FCB682A2C90B2D0120EBB7E3685B64B68190D855F3BCB625B5838DE090F0A97DACC8732E6093D151EF8B7B24A0A31214F284231DF2C164A26F809B54432DF2CA0126150288844F968E63F69CE779FFDC435ADFDDC57351E66E6B7E13748186DA9CDD6B20CC141EF2743FE065805A30DB04182694B47242BEC2D471789616EE25DE44607622043001D8F823207D09F1C60D6AEF642A0F5B7B1C90379A3D35920884714CE12A182318C48B09119A889CC7C73812A900AEEE368C482BC2BE9602D945E11995809158D879100223601737ED8169BC36ADDDD48506DA90D565B7F361AA96DA939A43A7E0009A62EB2D9086EDD01E9BDE0B6744C12CCDCE8749263FAF59593346B400C666765785595ECDA06C3B9FFD37A90F9686359252201D0E654E2C3FEACC8A4473F3564A27C3473667B5BD96907C0DFC89AADFAA276C3CC09C2FD9A02D2165B68E7D2C19AD2CC65C9901A758C6BCE9E24B6BE9C7617D8EA39ABBDC04ADA0DB531A97C9F696654654329F2A1194A5FF70B2DDFECC3619D8D9BA96E6ACFC60F1958ADAD7A10CC93CA88E5142811FF4B37AB2D10571CEB071C8E38DAA1C5BED9B0E633F3FC59759D4D5433B9B616D04BF8BED9914E0D981EF8287CBABD13FE7965F6BF202C9D89AE321C06A109816038DA7E44C06C9A0BAA761185114F6DE95BF6C398D6DC7376BDC9B9AE686E5616504DF25ADD916625881E7829798E7F289C14BECA3735084BB9AA36FA0AA9A8786EBF770E2B3C0C0E8ACF9C7381B1A157CD69B931574E4E89D3C038782D7195380C664B7D24CC1F7D4AD8AD7FD82920A9C6F561CF0CD7F87C1C08C755AE1EA6464FAA91AD79534459BDF786237509407D4880DE856554BB75E5E8FB1725B3BDBBA6D910E234E2FDBD914BCFA8844A4381FEC5AA32089BCB535D7F08419A93CE3D8E5CA9600C213A73D2A5695432231B73676111B94229EDBB1A6D23B4E50A682F77731A895E913B748D4A303A2B13CE238CADD218AFEB92E677E311567B63516E622555B0D357498DACF60C63DDB3AA2A130F91E06B1860D7ACC54B96C3D511AE70B4F8259A4561F9147F5B010D377C82595EC5679DBC3F3E793FF12ED0F121AB9CF66AC7B353365E8D9127DAC907EC890683D5946D6EEFCF86A1645940858F1504F0D76CF277116E36C4F4D50694B58C0EDBF881557DC45FB19A05E9EF5660F37B12984910E2E6F841E0CBC523BA8A03B8399FFCB36C73EA5DFDE34BDDEC9D779B22C139F58EBD7FF595774110C3EE5019457B88F5C02D4188FF4EF0081FB11EA0C91CC368D05480EB93E363EB5EC298629A6D4606228BA5537BCE37ACDFE1CD390FB11E38D3788955B01E43FB7193DE613D6024760AEB4308299F30773ED39E60EE74A34379BB41601CBFDC41511764761A9F682AD3FAB63AC2010BA6F910EB8F245CF6A1AE41F86F2F138B4D76EE36A9D854E72E523C7E195204B9FCBF9723FC97C80ADE1D56D4D3C2C166F7EE015A9BC1FB315CBA4D182285B7CB5C11A7EF7641459CBCDB091297BADB82D42E79A55FC70C64F338BB909ECAE2DC49C0D94CCDAFEC14AABEFA3F581912A637EEC43A710AE3D7290DB2E701072B0EE224BFDDF4823891EF2B13086976804395042EDD6D2786B139D43A1E3CE83C699D8051A969BBD9CE987C73FADD997BEAD9D7216364BA506C57C9BB27797532828813BC3A1903F9F4AEA3B2EFF2695A9DE9CEBCD6B0D3C454E3F19B8F0CEDB41B173BDAB69D79DF9649505F87AEE0328F765B4288ECA26EC7705B5D3FDE35D1323FE8EB10273A29A79B08745D77F6A5BB86CAB9C9BF6BD96992430A17D6AC6C99EB13C92EC4D6ED1044B324CEF21484BC1FE13C0D633F5C8348347EA6B2E1BCC0A36AC0B25F2EE11AC658E0D9319AF465F062A781CF9059478CDEF38F5A318FCFC044C16A1F885BA5F17412002B867464BE24522EDF8FC6E36F574C3F380DB24BC1D9B5E630169E71680DD39CB4462C1B485CC85C6104063639709D44C798951D454619648FEF4DE1C5B21381B1C9CBBB5FB16173C251C2639B0778E422A48DBA37524132CD4D3C024922F300F2A264930BF910644919B06F6CC2649B9F799FD224CA0D4A60E2920F7AD4F264941E647402E596A39A72A0744810CDC263F27BD2502D33133B0989325A63FFA2A28FA9C7F7A9F5ECDC87B80C77263B00A1D9F599CB496CC671FEB2CC5C3E6F3D20ED72878F4E4604E31C9170CC15AE993B900A8BF4ED43E8903A9D3301CC30FDFB41680B5974BF71A808C689B1B94767F3C1B1BCAB1D57A5FB61C324F59523E3F92478C49B98EA9AC3206FA9309BBD3499BDA813710653166C6BA9E64013B1E004E01579AAD93EE889C3F5C3B80D0BFA52E73464BB23CD585C67E44751578AA4CDB294F784C943D01D5B45DCA92629B2AC6BF2882CEB9BACA3E85C916D989F0CC4069C9F0464001391F037DFCD7A62D64E517F4C1549AFEAD4A322126F44D3A22E979052988794055D2B7F0E745D2E022DCE0F2A4E622A89D4C9E73135B832552C8D846F3FFD8153EAA2DB3251C850EED0EF96F0BE85274976AF8A51EA82A8C012C2864BEC69683A8E6AAEAF7AE6E65043E522168AD2AF2BAE59EC91945E8F88C324F6314461103F699E79D5F540E7E18A97317114C1DE86CE85B5938E5D69D1EE67F0FC422A89ACD77DF8D2386F8254DC2646D88E04906F6C65C1E67A20812AE899414672B90D4D623CA4631535C52AB248361EE27847039064A844F7DDB5FEDE4933EF2D753DB5C72352248C69F803A4A4EF2E03D40E9608EF643E5C8B7CF37C14A1B3E95D11E397E0D5AF4B9885CB16048E8F14439F32043475F0CCDF1A26188CB655D8A78F300701C8C1459A879838E8B38FB98B5561E926841D331E617015DF16F9BAC8D190E1EA31A2A408DB3554FD9F4D399CCF6ECBA7F4591F43406886F8F1FC6DFCB108A3A0C1FBB3C0CF4302021B4CEA37AD9897397EDBBA7C6920DD24B121A09A7C8D9DE71EA2C51501CB6EE305C0AF94ED717BC8E00F7009FC97791D0C4A0E44CF089AEC67972158A66095D530DAF6E82792E160B5F9F67F0D2D49FF66CE0000 , N'6.4.0')

