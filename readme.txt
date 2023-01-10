DeliveryDrivers

2023/01/07
[v]建立readme
[v]完成appsettings連線字串
[v]EntityFrameworkCore.Tools,EntityFrameworkCore.SqlSever,EntityFrameworkCore.Desin安裝完成
[v]建立EFModels
[v]刪除AppDbContext內的連線字串
[v]在Program.cs內註冊AppDbContext
[v]建立DeliveryDriversController
[v]修改DeliveryDrivers:Index
2023/01/08
[v]修改DeliveryDriversController
    [v]建立Index.cshtml
    [v]建立Details.cshtml
    [v]建立Create.cshtml
    [v]建立Edit.cshtml
    [v]建立Delete.cshtml
[v]建立DeliveryRecrodesController
    [v]建立Index.cshtml
    [v]建立Details.cshtml
    [v]建立Edit.cshtml
        [v]Edit表單嵌值完成待測試
[v]建立DeliveryViolationRecordsController
    [v]建立Index
    [v]建立Details
[v]建立DeliveryCancellationRecordsController
    [v]建立Index
    [v]建立Details

    todo
[]DeliveryDriversController
    []Edit無法正常改動，原因不明
[]DeliveryRecrodesController
    []建立Create
    []建立Delete
    []Details內的公里數需要以月計算
    []Edit存入DB
[]DeliveryViolationRecordsController
    []建立Create
    []建立Edit
    []建立Delete
[]DeliveryCancellationRecordsController
    []建立Create
    []建立Edit
    []建立Delete

Orders
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

Order_ver0.2.1 ~ 0.2.2
1. 修改OrdersController: 新增雙層式表單(Order => Details)(未完成, 但可檢視)
2. 修改View-Orders-index: 修改格式for雙層表單
 
Order_ver0.3(2023/01/10暫時展示版本)
1. 新增View-Orders-Details: 暫改使用新view頁面取代雙層表單呈現Details
2. 新增View-Orders-ProductsDetail: 新增產品說明(未完成)
3. 修改OrdersController: 新增method以執行detail與ProductsDetail頁面