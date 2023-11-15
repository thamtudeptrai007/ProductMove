- Chuẩn bị database
	Sử dụng Micrsoft SQL Server:
		Tài khoản: sa
		Mật khẩu: 123456
	Hoặc thay đổi tài khoản ở file ProductMove_API/appsettings.json
- Run project:	
	Sử dung Micrsoft Visual Studio 2022 
	Mở solution ProductMove chúng ta sẽ có 3 project con bên trong để trang web được hoạt động cần chạy đồng thời
	hai project là ProductMove_API và ProductMove_App. 
	Right click lên Solution ProductMove ---> Set Startup Project  ---> Multiple startup project 
	--> chọn Action start cho 2 project ProductMove_API và ProductMove_App , project ProductMove_Mode để mặc định None 
	-->Apply ---> OK 
	Sau đó run project như bình thường