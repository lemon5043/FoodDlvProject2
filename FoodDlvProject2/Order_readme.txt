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
**1. 更新Database
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

Order_ver0.4
**1. 分支合併main, 重新建立分支

Order_ver0.4.1
1. 修改OrdersController: 修改雙層式表單(Order => Details)(未完成, 但可檢視)
2. 修改View-Orders-index: 修改格式for雙層表單

Order_ver0.5
**1. 更新Database, 全部內容重做
2. 更新EFModels
3. 重新設計OrderVM, OrderDetailVM
4. 製作三層式架構(製作中)

Oreder_ver0.5.1 ~ 0.5.8
1. 重新設計view-index, 主頁捨棄雙層式表單
2. 修正MVC三層式架構
	2-1. Controller: Order
	2-2. ViewModel: Order, OrderDetail, Product
	2-3. Service: Order
	2-4. Data Treanfer Object: Order, OrderDetail, OrderProduct, OrderSchedule
	2-5. Interface: OrderRepository
	2-6. Repository: Order
	2-7. View: index.html, index.css, DetailIndex
3. 新增
3. 新增功能index頁面: 
	3-1. 日期範圍搜尋
	3-2. 關鍵字模糊搜尋(姓名,店家名稱,地址)
	3-3. 連結DetailIndex
	3-4. 訂單狀態-時間線(未完成)
	3-5. 非同步顯示(未完成)
	