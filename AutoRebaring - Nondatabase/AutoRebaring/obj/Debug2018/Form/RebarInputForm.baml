   M S B A M L   `   `   `  ���� J  FAutoRebaring, Version=2017.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:AutoRebaring.FormAutoRebaring.Form  &     AutoRebaring.Form.RebarInputForm   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1localclr-namespace:AutoRebaring.Form     $��1142.926��$	��950��.��� .+��� $��0,0,0,0q�$��Visible=�.����   �� Orientation$  Vertical=�.+��� $  
Horizontal=�.+��� $��ĐĂNG NHẬP��$
��Left=�$	��100��$��0,0,0,0q�$	��Top=�$	��327��$��Normal"�.���� $  
Horizontal=�.+��� $  Vertical=�.+��� $  
Horizontal=�.+��� $��Tên dự án:���� -    ��Name$ 
cbbProject��$
��Left=�$��50,0,0,0q�$	��Top=�$	��130���� $  
Horizontal=�.+��� $��Tên đăng nhập:���� -    ��Name$ txtUserName��$��53,0,0,0q� �� TextAlignment$ Right=�$
��Left=�$��23�� �� TextWrapping$ NoWrap=�$�� ��$	��Top=�$	��100���� $  
Horizontal=�.+��� $��Mật khẩu:��G� -    G�Name$ txtPassword��$��81,0,0,0q� �� TextAlignment$ Right=�$
��Left=�$	��Top=� G� PasswordChar$ *��$	��100��$��23���� $  Vertical=�.+��� -    ��Name$ 	btnLogout��$��Log out��$��25��$��Center=�$��	15,10,0,7q�$��Bottom=�$��47���� -   $ btnCheckUser��$��OK��$��25��$��Center=�$��15,0,0,7q�$��Bottom=�$��47���� -   	 ��Name$	 colParamGrb��$&�� THAM BIẾN KÍCH THƯỚC CỘT��$
��Left=�$	��100��$��0,0,0,0q�$	��Top=�$	��204��$��Normal"�.���� $  
Horizontal=�.+��� $  Vertical=�.+��� $  
Horizontal=�$��0,0,0,0q�.+��� $	��B1:��$��39���� -   $ cbbB1��$
��Left=�$��0,0,0,0q�$	��Top=�$��75���� $  
Horizontal=�$��0,0,0,0q�.+��� $	��B2:��$��39���� -   $ cbbB2��$
��Left=�$��0,0,0,0q�$	��Top=�$��74���� $  
Horizontal=�$��0,0,0,0q�.+��� $��Mark:��$��39���� -	   $ cbbMark��
 �� 
IsEditable
 . $��74��$
��Left=�$��22��$��0,0,0,0q� �� Text$  ��$��Center=��� $  
Horizontal=�.+��� $  Vertical=�.+��� -
   $	 genParamGrb��$��THÔNG SỐ CHUNG��$
��Left=�$	��356��$��0,0,0,0q�$	��Top=�$	��373��$��Normal"�.���� $  
Horizontal=�.+��� $  Vertical=�.+��� $��Include��$��
315,-5,0,0q�$
��Bold"��� $  
Horizontal=�.+��� $��Bêtông bảo vệ:��$
��Left=�$	��Top=��� -   $ txtConcCover��$��Center=�$��23��$
 Wrap=�$�� ��$ Right=�$��38���� $��mm��$
��Left=�$	��Top=��� $  
Horizontal=�.+��� $��Chiều dài nối thép:��$
��Left=�$	��Top=�$��Normal"��� -   $ txtDevMulti��$
��Left=�$��23��$��Center=�$
 Wrap=�$�� ��$ Right=�$��27��$��Normal"��� $��d��$
��Left=�$	��Top=��� $  
Horizontal=�.+��� $(��"Khoảng cách các đoạn nối:��$
��Left=�$	��Top=��� -   $ txtDevsDist��$
��Left=�$��23��$
 Wrap=�$�� ��$ Right=�$��Center=�$��27���� $��mm��$
��Left=�$	��Top=��� $  
Horizontal=�.+��� $*��$Thép đai gia cường vùng nối:��$
��Left=�$	��Top=��� -    ��Name$ chkReinfStirrInclude��$��Center=�$��	168,0,0,0q��� $  
Horizontal=�.+��� $��Nối phạm vùng dầm:��$
��Left=�$	��Top=��� $��Delta:��$
��Left=�$	��Top=�$��42���� -   $ txtDelDevError��$
��Left=�$��23��$
 Wrap=�$�� ��$ Right=�$��Center=�$��27��{�.  �� $��mm��$
��Left=�$	��Top=��� $��
Số lần��$
��Left=�$	��Top=��� -   $ txtNumDevError��$
��Left=�$��23��$
 Wrap=�$�� ��$ Right=�$��Center=�$��27��{�.  �� -   $ chkDevErrorInclude��$��Center=�$��41,0,0,0q��� $  
Horizontal=�.+��� $(��"Vị trí nối thép cách level:��$
��Left=�$	��Top=��� -   $ txtDevLevelOffAllow��$
��Left=�$��23��$
 Wrap=�$�� ��$ Right=�$��Center=�$��36��{�.  �� $��mm��$
��Left=�$	��Top=��� -   $ chkDevLevelOffInclude��$��Center=�$��	125,0,0,0q��� $  
Horizontal=�.+��� $&�� Bóp tiết diện giới hạn:��$
��Left=�$	��Top=��� -   $ txtShortenLimit��$
��Left=�$��23��$
 Wrap=�$�� ��$ Right=�$��Center=�$��38���� $��mm��$
��Left=�$	��Top=��� $  
Horizontal=�.+��� $��Chiều dài neo thép:��$
��Left=�$	��Top=��� -   $ txtAncMulti��$
��Left=�$��23��$
 Wrap=�$�� ��$ Right=�$��Center=�$��27���� $��d��$
��Left=�$	��Top=��� $  
Horizontal=�.+��� $ ��Chiều dài khóa đầu:��$
��Left=�$	��Top=��� -   $ txtLockHeadMulti��$
��Left=�$��23��$
 Wrap=�$�� ��$ Right=�$��Center=�$��27���� $��d��$
��Left=�$	��Top=��� $  
Horizontal=�.+��� $<��6Khoảng cách mép sàn tới vị trí khóa đầu:��$
��Left=�$	��Top=��� -   $ txtConcTopCover��$
��Left=�$��23��$
 Wrap=�$�� ��$ Right=�$��Center=�$��38���� $��mm��$
��Left=�$	��Top=��� $  
Horizontal=�.+��� $!��Tỉ lệ LH thép bẻ ke:��$
��Left=�$	��Top=��� -   $ 
txtRatioLH��$
��Left=�$��23��$
 Wrap=�$�� ��$ Right=�$��Center=�$��23���� $  
Horizontal=�.+��� $8��2Khoảng cách mép sàn đến vị trí bẻ ke:��$
��Left=�$	��Top=��� -   $ txtCoverTopSmall��$
��Left=�$��23��$
 Wrap=�$�� ��$ Right=�$��Center=�$��38���� $��mm��$
��Left=�$	��Top=��� -   $	 devRebarGrb��$��VỊ TRÍ NỐI THÉP��$	��213��$	��402��$
��Left=�$	��Top=�$��0,0,0,0q�.���� $  Vertical=�.+��� $  
Horizontal=�.+��� $��Thép dọc��$��	105,0,0,0q��� $��Include��$��11,0,0,0q�$
��Bold"��� $��
Thép đai��$��50,0,0,0q��� $��Include��$��11,0,0,0q�$
��Bold"��� $  
Horizontal=�.+��� $��Bottom Offset:���� -   $ 	txtBotOff��$��12,0,0,0q�$
��Left=�$��23��$
 Wrap=�$�� ��$ Right=�$��Center=�$��38��{�.  �� $��mm��$
��Left=�$	��Top=��� -   $ chkOff��$��22,0,0,0q�$
��Left=�$��Center=��� -   $ txtBotOffStir��$��62,0,0,0q�$
��Left=�$��23��$
 Wrap=�$�� ��$ Right=�$	��Top=�$��38��{�.  �� $��mm��$
��Left=�$	��Top=��� -   $ 
chkOffStir��$
��Left=�$��22,0,0,0q�$��Center=��� $  
Horizontal=�.+��� $��L/��$��	110,0,0,0q��� -   $ txtBotOffRatio��$��2,0,0,0q�$
��Left=�$��23��$
 Wrap=�$�� ��$ Right=�$��Center=�$��27��{�.  �� -    $ chkOffRatio��$��32,0,0,0q�$�� ��$
��Left=�$��Center=��� $��L/��$��66,0,0,0q��� -!   $ txtBotOffRatioStir��$
��Left=�$��23��$��2,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��27��{�.  �� -"   $ chkOffRatioStir��$��32,0,0,0q�$�� ��$
��Left=�$��Center=��� $��0,20,0,0q�$  
Horizontal=�.+��� $��Top Offset:���� -#   $ 	txtTopOff��$��32,0,0,0q�$
��Left=�$��23��$
 Wrap=�$�� ��$ Right=�$��Center=�$��38��{�.  �� $��mm��$
��Left=�$	��Top=��� -$   $ chkOff2��$��22,0,0,0q�$
��Left=�$��Center=��� -%   $ txtTopOffStir��$
��Left=�$��23��$��62,0,0,0q�$
 Wrap=�$�� ��$ Right=�$	��Top=�$��38��{�.  �� $��mm��$
��Left=�$	��Top=��� -&   $ chkOffStir2��$
��Left=�$��22,0,0,0q�$��Center=��� $  
Horizontal=�.+��� $��L/��$��	110,0,0,0q��� -'   $ txtTopOffRatio��$
��Left=�$��23��$��2,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��27��{�.  �� -(   $ chkOffRatio2��$��32,0,0,0q�$�� ��$
��Left=�$��Center=��� $��L/��$��66,0,0,0q��� -)   $ txtTopOffRatioStir��$
��Left=�$��23��$��2,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��27��{�.  �� -*   $ chkOffRatioStir2��$��32,0,0,0q�$�� ��$
��Left=�$��Center=��� $  
Horizontal=�$��0,18,0,0q�.+��� $��Bỏ qua cao độ dầm���� -+   $ chkInsideBeam��$
��Left=�$��78,0,0,0q�$��Center=��� -,   $ chkStirInsideBeam��$
��Left=�$��	150,0,0,0q�$��Center=��� $  Vertical=�.+��� $  
Horizontal=�.+��� --   $	 rebarChosenGrb��$��CHỌN THÉP��$	��388��$	��467��$��0,0,0,0q�$	��Top=�$
��Left=�.���� $  Vertical=�.+��� $��Thép dọc:��$
��Bold"��� $��Phối thép chuẩn:���� $  
Horizontal=�.+��� $	��L =���� -.   $ txtStandardL0��$
��Left=�$��23��$��52,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� -/   $ txtStandardL1��$
��Left=�$��23��$��5,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� -0   $ txtStandardL2��$
��Left=�$��23��$��5,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� -1   $ txtStandardL3��$
��Left=�$��23��$��5,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� -2   $ txtStandardL4��$
��Left=�$��22��$��5,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� -3   $ txtStandardL5��$
��Left=�$��22��$��5,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� -4   $ txtStandardL6��$
��Left=�$��22��$��5,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� $  
Horizontal=�.+��� $��L1+L2 =���� -5   $ txtStandardPairL0��$
��Left=�$��23��$��25,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� -6   $ txtStandardPairL1��$
��Left=�$��23��$��5,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� -7   $ txtStandardPairL2��$
��Left=�$��23��$��5,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� -8   $ txtStandardPairL3��$
��Left=�$��23��$��5,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� -9   $ txtStandardPairL4��$
��Left=�$��22��$��5,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� -:   $ txtStandardPairL5��$
��Left=�$��22��$��5,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� -;   $ txtStandardPairL6��$
��Left=�$��22��$��5,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� $  
Horizontal=�.+��� $��
L1+L2+L3 =���� -<   $ txtStandardTripL0��$
��Left=�$��23��$��5,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� -=   $ txtStandardTripL1��$
��Left=�$��23��$��5,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� ->   $ txtStandardTripL2��$
��Left=�$��23��$��5,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� -?   $ txtStandardTripL3��$
��Left=�$��23��$��5,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� -@   $ txtStandardTripL4��$
��Left=�$��22��$��5,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� -A   $ txtStandardTripL5��$
��Left=�$��22��$��5,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� -B   $ txtStandardTripL6��$
��Left=�$��22��$��5,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� $��Phối thép dư:���� $  
Horizontal=�.+��� $��Lmax:���� -C   $ txtLmax��$
��Left=�$��23��$��0,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��38���� $��Lmin:��$��25,0,0,0q��� -D   $ txtLmin��$
��Left=�$��23��$��0,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��38���� $��mm��$
��Left=�$	��Top=��� $��Step:��$��25,0,0,0q��� -E   $ txtStep��$
��Left=�$��23��$��0,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��38���� $��mm��$
��Left=�$	��Top=��� $��Thép cắm chờ:��$
��Bold"��� $  
Horizontal=�.+��� $��Phối thép chuẩn:���� $��Lmax:��$��15,0,0,0q��� -F   $ txtImplantLmax��$
��Left=�$��23��$��0,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��38���� $��mm��$
��Left=�$	��Top=��� $  
Horizontal=�.+��� $	��L =���� -G   $ txtImplantL0��$
��Left=�$��23��$��52,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� -H   $ txtImplantL1��$
��Left=�$��23��$��5,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� -I   $ txtImplantL2��$
��Left=�$��23��$��5,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� -J   $ txtImplantL3��$
��Left=�$��23��$��5,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� -K   $ txtImplantL4��$
��Left=�$��23��$��5,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� -L   $ txtImplantL5��$
��Left=�$��23��$��5,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� -M   $ txtImplantL6��$
��Left=�$��23��$��5,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� $  
Horizontal=�.+��� $��L1+L2 =���� -N   $ txtImplantPairL0��$
��Left=�$��23��$��25,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� -O   $ txtImplantPairL1��$
��Left=�$��23��$��5,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� -P   $ txtImplantPairL2��$
��Left=�$��23��$��5,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� -Q   $ txtImplantPairL3��$
��Left=�$��23��$��5,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� -R   $ txtImplantPairL4��$
��Left=�$��23��$��5,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� -S   $ txtImplantPairL5��$
��Left=�$��23��$��5,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� -T   $ txtImplantPairL6��$
��Left=�$��23��$��5,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��49���� $��Thép đai:��$
��Bold"��� $��Loại family:���� $  
Horizontal=�.+��� $��Đai ngoài:���� -U   $ cbbFamilyStirrup1��$
��Left=�$��7,0,0,0q�$��Center=�$	��115���� $��Đai trong:��$��30,0,0,0q��� -V   $ cbbFamilyStirrup2��$
��Left=�$��7,0,0,0q�$	��Top=�$	��131���� -W   $	 otherParametersGrb��$��CÁC THÔNG SỐ KHÁC��$	��241��$	��226��$
��Left=�$	��Top=�$��0,0,0,0q�.���� $  Vertical=�.+��� $��Kiểm tra tiết diện��$
��Bold"��� $  
Horizontal=�.+��� $��Tầng:���� -X   $ cbbCheckLevel��$
��Left=�$��0,0,0,0q�$��Center=�$��99���� $  
Horizontal=�.+��� $  Vertical=�.+��� $  
Horizontal=�.+��� $	��B1:���� -Y    ��Name$ 
lblCheckb1��$ Right=�$	��b2=��$
��Left=�$��55,0,0,0q�$	��Top=�$��42���� $  
Horizontal=�.+��� $	��B2:���� -Z   $ 
lblCheckb2��$ Right=�$	��b2=��$
��Left=�$��55,0,0,0q�$	��Top=�$��42���� -[   $ Reverse��$��Nghịch đảo��$
��Left=�$��15,0,0,0q�$��Center=�$��69���� $%��Hiển thị thép trên View3d��$
��Bold"��� $  
Horizontal=�.+��� -\   $ 	chkView3d��$��Yes/No��$
��Left=�$��0,0,0,0q�$��Center=��� -]   $ 	cbbView3d��$
��Left=�$��20,0,0,0q�$��Center=�$	��149���� $!��Các tham biến liên quan��$
��Bold"��� $  
Horizontal=�.+��� $  Vertical=�.+��� $!��Số lượng cấu kiện:���� $  Vertical=�.+��� -^   $ txtPartCount��$
��Left=�$��23��$��15,0,0,0q�$
 Wrap=�$��1��$ Right=�$��Center=�$��45���� -_   $	 levelTitleGrb��$��QUY ƯỚC TẦNG��$	��706��$��88��$
��Left=�$	��Top=�$��0,0,0,0q�.���� $��0,0,0,0q���. $
��Auto=�$��Hidden=�.���� -`    ��Name$ spFull��$  
Horizontal=�$
��Left=�$	��Top=�$	��666���� $  
Horizontal=�.+��� -a   $	 rebarDesGrb��$��THÉP THIẾT KẾ��$	��458��$	��751��$
��Left=�$	��Top=�$��0,0,0,0q�.���� $  Vertical=�.+��� $  Vertical=�.+��� $��Phạm vi thiết kế:��$
��Bold"��� $  
Horizontal=�.+��� $��Bắt đầu:���� -b   $ cbbStartLevel��$
��Left=�$��Center=�$	��121���� $��Kết thúc:��$��10,0,0,0q��� -c   $ cbbEndLevel��$
��Left=�$��Center=�$	��120���� $  
Horizontal=�.+�	� -d    	�Name$ rbtLockHead��$��Khóa đầu��$��	205,0,0,0q�	� -e   $ rbtStartRebar��$��Thép chờ��$��15,0,0,0q��� $6��0Thép chờ cách cao độ tầng bắt đầu:��$
��Bold"��� $  
Horizontal=�.+��� $	��Z1=���� -f   $ txtRebarOff1��$
��Left=�$��23��$��0,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��38���� $��mm��$
��Left=�$	��Top=��� $	��Z2=��$��30,0,0,0q��� -g   $ txtRebarOff2��$
��Left=�$��23��$
 Wrap=�$�� ��$ Right=�$��Center=�$��38���� $��mm��$
��Left=�$	��Top=��� $��Thép bố trí:��$
��Bold"��� $  
Horizontal=�.+��� $��Tầng bắt đầu:��$��Normal"��� -h   $ cbbDesignLevel0��$
��Left=�$��15,0,0,0q�$��Center=�$��75���� -i   $ cbbDesignLevel1��$
��Left=�$��5,0,0,0q�$��Center=�$��75���� -j   $ cbbDesignLevel2��$
��Left=�$��5,0,0,0q�$��Center=�$��75���� -k   $ cbbDesignLevel3��$
��Left=�$��5,0,0,0q�$��Center=�$��75���� -l   $ cbbDesignLevel4��$
��Left=�$��5,0,0,0q�$��Center=�$��75���� -m   $ cbbDesignLevel5��$
��Left=�$��5,0,0,0q�$��Center=�$��75���� -n   $ cbbDesignLevel6��$
��Left=�$��5,0,0,0q�$��Center=�$��75���� -o   $ cbbDesignLevel7��$
��Left=�$��5,0,0,0q�$��Center=�$��75���� $  
Horizontal=�.+��� $��Kiểu thép dọc:��$��Normal"��� -p   $ cbbDesignRebarType0��$
��Left=�$��12,0,0,0q�$��Center=�$��75���� -q   $ cbbDesignRebarType1��$
��Left=�$��5,0,0,0q�$��Center=�$��75���� -r   $ cbbDesignRebarType2��$
��Left=�$��5,0,0,0q�$��Center=�$��75���� -s   $ cbbDesignRebarType3��$
��Left=�$��5,0,0,0q�$��Center=�$��75���� -t   $ cbbDesignRebarType4��$
��Left=�$��5,0,0,0q�$��Center=�$��75���� -u   $ cbbDesignRebarType5��$
��Left=�$��5,0,0,0q�$��Center=�$��75���� -v   $ cbbDesignRebarType6��$
��Left=�$��5,0,0,0q�$��Center=�$��75���� -w   $ cbbDesignRebarType7��$
��Left=�$��5,0,0,0q�$��Center=�$��75���� $  
Horizontal=�.+��� $��Số lượng B1:��$��Normal"��� -x   $ txtDesignN10��$
��Left=�$��23��$��46,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -y   $ txtDesignN11��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -z   $ txtDesignN12��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -{   $ txtDesignN13��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -|   $ txtDesignN14��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -}   $ txtDesignN15��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -~   $ txtDesignN16��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -   $ txtDesignN17��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� $  
Horizontal=�.+��� $��Số lượng B2:��$��Normal"��� -�   $ txtDesignN20��$
��Left=�$��23��$��46,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignN21��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignN22��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignN23��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignN24��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignN25��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignN26��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignN27��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� $  
Horizontal=�.+��� $��Tổng số lượng:��$��Normal"��� -�   $ txtDesignSum0��$	��b2=��$
��Left=�$��50,0,0,0q�$ Right=�$��Center=��� -�   $ txtDesignSum1��$	��b2=��$
��Left=�$��49,0,0,0q�$ Right=�$��Center=��� -�   $ txtDesignSum2��$	��b2=��$
��Left=�$��49,0,0,0q�$ Right=�$��Center=��� -�   $ txtDesignSum3��$	��b2=��$
��Left=�$��49,0,0,0q�$ Right=�$��Center=��� -�   $ txtDesignSum4��$	��b2=��$
��Left=�$��49,0,0,0q�$ Right=�$��Center=��� -�   $ txtDesignSum5��$	��b2=��$
��Left=�$��49,0,0,0q�$ Right=�$��Center=��� -�   $ txtDesignSum6��$	��b2=��$
��Left=�$��49,0,0,0q�$ Right=�$��Center=��� -�   $ txtDesignSum7��$	��b2=��$
��Left=�$��49,0,0,0q�$ Right=�$��Center=��� $  
Horizontal=�.+��� $��Kiểu đai ngoài:��$��Normal"��� -�   $ cbbDesignStirrupType10��$
��Left=�$��10,0,0,0q�$��Center=�$��75���� -�   $ cbbDesignStirrupType11��$
��Left=�$��5,0,0,0q�$��Center=�$��75���� -�   $ cbbDesignStirrupType12��$
��Left=�$��5,0,0,0q�$��Center=�$��75���� -�   $ cbbDesignStirrupType13��$
��Left=�$��5,0,0,0q�$��Center=�$��75���� -�   $ cbbDesignStirrupType14��$
��Left=�$��5,0,0,0q�$��Center=�$��75���� -�   $ cbbDesignStirrupType15��$
��Left=�$��5,0,0,0q�$��Center=�$��75���� -�   $ cbbDesignStirrupType16��$
��Left=�$��5,0,0,0q�$��Center=�$��75���� -�   $ cbbDesignStirrupType17��$
��Left=�$��5,0,0,0q�$��Center=�$��75���� $  
Horizontal=�.+��� $��Bước trên/dưới:��$��Normal"��� -�   $ txtDesignStirrupTB10��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignStirrupTB11��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignStirrupTB12��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignStirrupTB13��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignStirrupTB14��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignStirrupTB15��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignStirrupTB16��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignStirrupTB17��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� $  
Horizontal=�.+��� $��Bước giữa:��$��Normal"��� -�   $ txtDesignStirrupM10��$
��Left=�$��23��$��57,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignStirrupM11��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignStirrupM12��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignStirrupM13��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignStirrupM14��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignStirrupM15��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignStirrupM16��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignStirrupM17��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� $  
Horizontal=�.+��� $��Kiểu đai trong:��$��Normal"��� -�   $ cbbDesignStirrupType20��$
��Left=�$��10,0,0,0q�$��Center=�$��75���� -�   $ cbbDesignStirrupType21��$
��Left=�$��5,0,0,0q�$��Center=�$��75���� -�   $ cbbDesignStirrupType22��$
��Left=�$��5,0,0,0q�$��Center=�$��75���� -�   $ cbbDesignStirrupType23��$
��Left=�$��5,0,0,0q�$��Center=�$��75���� -�   $ cbbDesignStirrupType24��$
��Left=�$��5,0,0,0q�$��Center=�$��75���� -�   $ cbbDesignStirrupType25��$
��Left=�$��5,0,0,0q�$��Center=�$��75���� -�   $ cbbDesignStirrupType26��$
��Left=�$��5,0,0,0q�$��Center=�$��75���� -�   $ cbbDesignStirrupType27��$
��Left=�$��5,0,0,0q�$��Center=�$��75���� $  
Horizontal=�.+��� $��Bước trên/dưới:��$��Normal"��� -�   $ txtDesignStirrupTB20��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignStirrupTB21��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignStirrupTB22��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignStirrupTB23��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignStirrupTB24��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignStirrupTB25��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignStirrupTB26��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignStirrupTB27��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� $  
Horizontal=�.+��� $��Bước giữa:��$��Normal"��� -�   $ txtDesignStirrupM20��$
��Left=�$��23��$��57,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignStirrupM21��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignStirrupM22��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignStirrupM23��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignStirrupM24��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignStirrupM25��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignStirrupM26��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ txtDesignStirrupM27��$
��Left=�$��23��$��30,0,0,0q�$
 Wrap=�$�� ��$ Right=�$��Center=�$��50���� -�   $ btnOK��$��OK��$��Center=�$��
130,0,0,20q�$��Bottom=�$	��123��$��48��$��36(�$
��Bold"�