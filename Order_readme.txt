Order訂單管理後台

ver0.1
1. 環境建置:
	1-1. Nuget套件管理: 新增EFCore sqlserver與EFCore tool(版本6.0.12)
	1-2. 更新Program與appsetting: 新增連線字串
2. entity.framework建置基礎版型
	2-1. 新增EFModels: 使用EFCore Power tools新增models
	2-2. 新增OrdersController: 使用EF製作基礎版型

ver0.1.1 ~ 0.1.2
1. 更新環境:
	1-1. Nuget套件管理:新增EFCore Design(版本6.0.12)
	1-2. 更新連線字串
	1-3. 更新Database
	1-4. 重新建置EFModels: 使用指令建置

ver0.2
1. 更新Database
2. 更新EFModels
3. 新增OrderVM, OrderDetailVM
4. 重做OrdersController: 
	4-1. 新增index: 具備基本檢視清單功能
5. 新增View-Orders-index
6. 製作三層式架構:
	6-1. 新增Infrastructures-ExtensionMethods-OrderExts

ver0.2.1 ~ 0.2.2
1. 修改OrdersController: 新增雙層式表單(Order => Details)(未完成, 但可檢視)
2. 修改View-Orders-index: 修改格式for雙層表單
 
ver0.3(2023/01/10暫時展示版本)
1. 新增View-Orders-Details: 暫改使用新view頁面取代雙層表單呈現Details
2. 新增View-Orders-ProductsDetail: 新增產品說明(未完成)
3. 修改OrdersController: 新增method以執行detail與ProductsDetail頁面

ver1.0
1. 2nd專題發表後合併版本再次新切分支
2. 修改Readme目錄位置

ver1.0.1
1. 新增OrderMain相關物件

ver1.1
***架構與Order-layout重新設計(實作中)
1. OrderTracking(訂單搜尋系統)
	1-1. OrderController-index => OrderController-OrderTracking
	1-2. Order-View-index => Order-View-OrderTracking
	1-3. 關鍵字搜尋改版 => 可以針對欄位做格別關鍵字搜尋
	1-4. 新增訂單狀態顯示 => 可以顯示每筆訂單當下狀態, 依據每種狀態變換badge顯示顏色
	1-5. OrderSchedule狀態時間線獨立Partial View, 使用ajax呼叫來達成非同步顯示
	1-6. PageList功能移至repository, 實現資料傳遞效率提升