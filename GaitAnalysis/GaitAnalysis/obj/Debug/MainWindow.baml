   M S B A M L   `   `   `  ����  GaitAnalysis    GaitAnalysis.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          �Name$  frmMainWindow��5   	    � Title$ Gait Analysis��5   	   $��650.5��6   $
��1064��6.    � WindowStartupLocation$ CenterScreen=�5   7    � WindowStyle$ ThreeDBorderWindow=�6\   .��5      � .+�5   
   �� -   5         ��Name$ 	imgCamera��5      $	��540��6#   $��	0,30,10,0q�60   $	��Top=�6C   $��Right=�6[   $	��960��6w   �� 5   
   -   5        $��canvas��5      $	��960��6   $	��540��6+   $��	0,30,10,0q�68   $��Right=�6K   $	��Top=�6g   �� 5	   
   -   5         ��Name$ tbFrameRate��5	       �� TextWrapping$
 Wrap=�6'   $	��170��6;    �� Left$	 780��6G   
 �� Top$ 10��6Y   $��869,30,17,561q�6i   {�.  6�   �� 5   
   -   5        	 ��Name$	 	btnRecord��5      $��Record��6$   $��Right=�65   $��75��6Q   $��	0,0,17,16q�6\   $��20��6�   $��Bottom=�6�   
 �� 	IsDefault
 .  � ToolTip$ Record Data��6�   �� 5   
   -   5        $	 	btnReport��5      $��Report��6$   $��
0,0,111,16q�65   $��Right=�6a   $��75��6}   $��20��6�   $��Bottom=�6�   �� 5   
   -   5        $	 	btnCancel��5      $��Cancel��6$   $��	10,0,0,16q�65    �� IsCancel . $
��Left=�6X   $��75��6s   $��20��6~   $��Bottom=�6�   6�   o� 5   
   $
��Left=�6   $��25��6*   $	��Top=�66   $
��1049��6N   .@�5      m� $
��File��6   $
 File��6%   $��5,3,0,0q�64   .@�5      m� -   5        $	��New��5      6(   m� 5      -	   5        $
��Open��5      6)   m� 5      -
   5        $��
Report Bug��5      6/   m� 5      -   5        $
��Exit��5      6)   5      m� 5      $
��Edit��6   $
 File��6%   $��5,3,0,0q�64   .@�5      m� -   5        $��Gait Standard Parameters��5      6=   m� 5      -   5        $��Peson Information��5      66   5      5      5      5      