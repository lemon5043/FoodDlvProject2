order_ver0.1
1. 環境建置:
	1-1. Nuget套件管理: 新增EFCore sqlserver與EFCore tool(版本6.0.12)
	1-2. 更新Program與appsetting: 新增連線字串
2. entity.framework建置基礎版型
	2-1. 新增EFModels: 使用EFCore Power tools新增models
	2-2. 新增OrdersController: 使用EF製作基礎版型

Order_ver0.1.1 ~ 0.1.2
1. 更新環境:
	1-1. Nuget套件管理:新增EFCore Design(版本6.0.12)
	1-2. 更新連線字串
	1-3. 更新Database
	1-4. 重新建置EFModels: 使用指令建置

Order_ver0.2
1. 更新Database
2. 更新EFModels
3. 新增OrderVM, OrderDetailVM
4. 重做OrdersController: 
	4-1. 新增index: 具備基本檢視清單功能
5. 新增View-Orders-index
6. 製作三層式架構:
	6-1. 新增Infrastructures-ExtensionMethods-OrderExts
	