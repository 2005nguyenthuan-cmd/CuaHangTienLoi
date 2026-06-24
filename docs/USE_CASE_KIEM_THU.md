# Use Case kiểm thử phần mềm Cửa Hàng Tiện Lợi

## 1. Phạm vi kiểm thử

Tài liệu này dùng để kiểm thử các chức năng chính của phần mềm quản lý cửa hàng tiện lợi:

- Đăng nhập và phân quyền.
- Tổng quan dashboard và xuất báo cáo Excel.
- Bán hàng POS, giỏ hàng, khuyến mãi, thanh toán.
- Quản lý sản phẩm và ảnh sản phẩm.
- Quản lý kho, tồn kho thấp, nhập hàng.
- Quản lý nhà cung cấp.
- Quản lý khách hàng và điểm tích lũy.
- Quản lý nhân viên, ca làm, phân công công việc.
- Lịch sử hóa đơn.

## 2. Dữ liệu kiểm thử gợi ý

| Nhóm dữ liệu | Dữ liệu mẫu |
|---|---|
| Tài khoản quản lý | Tài khoản có vai trò `Quản lý` trong database |
| Tài khoản nhân viên | Tài khoản có vai trò `Nhân viên` trong database |
| Sản phẩm | Coca Cola, Pepsi, Sting, Mì Hảo Hảo, Sữa Vinamilk, Bánh Chocopie |
| Khuyến mãi | Mã còn hiệu lực, mã hết hạn, mã không tồn tại |
| Khách hàng | Số điện thoại đã đăng ký, số điện thoại chưa đăng ký |
| Ngày báo cáo | Hôm nay, 7 ngày gần nhất, 30 ngày gần nhất, khoảng ngày tùy chọn |

## 3. Danh sách use case tổng quát

| ID | Use case | Tác nhân | Mục tiêu |
|---|---|---|---|
| UC-01 | Đăng nhập hệ thống | Quản lý, nhân viên | Người dùng đăng nhập vào phần mềm |
| UC-02 | Xem dashboard | Quản lý | Xem doanh thu, đơn hàng, lợi nhuận, tồn kho |
| UC-03 | Xuất báo cáo Excel | Quản lý | Xuất file báo cáo có dữ liệu theo khoảng ngày |
| UC-04 | Xem danh sách sản phẩm POS | Nhân viên | Hiển thị sản phẩm, ảnh, giá, tồn kho |
| UC-05 | Thêm sản phẩm vào giỏ hàng | Nhân viên | Chọn sản phẩm và tính tiền |
| UC-06 | Xóa sản phẩm khỏi giỏ hàng | Nhân viên | Xóa dòng sản phẩm đã chọn |
| UC-07 | Áp dụng mã khuyến mãi | Nhân viên | Giảm giá hóa đơn theo mã hợp lệ |
| UC-08 | Thanh toán hóa đơn | Nhân viên | Lưu hóa đơn và trừ tồn kho |
| UC-09 | Tìm khách hàng khi thanh toán | Nhân viên | Nhận diện khách hàng bằng số điện thoại |
| UC-10 | Thêm/sửa/xóa sản phẩm | Quản lý | Quản lý thông tin sản phẩm |
| UC-11 | Chọn ảnh sản phẩm | Quản lý | Lưu và hiển thị ảnh sản phẩm |
| UC-12 | Quản lý kho | Quản lý | Xem tồn kho, nhập hàng, cảnh báo tồn thấp |
| UC-13 | Quản lý nhà cung cấp | Quản lý | Thêm, sửa, xóa thông tin nhà cung cấp |
| UC-14 | Quản lý khách hàng | Quản lý, nhân viên | Thêm khách hàng và theo dõi điểm |
| UC-15 | Quản lý mã khuyến mãi | Quản lý | Tạo, sửa, xóa mã giảm giá |
| UC-16 | Xem lịch sử hóa đơn | Quản lý, nhân viên | Tra cứu hóa đơn đã bán |
| UC-17 | Quản lý nhân viên | Quản lý | Thêm, sửa, xóa nhân viên và tài khoản |
| UC-18 | Phân công công việc | Quản lý | Giao việc theo ngày/ca cho nhân viên |
| UC-19 | Chỉnh sửa lịch làm | Quản lý | Điều chỉnh ca làm, tăng ca, ngày nghỉ |
| UC-20 | Báo cáo ca làm | Quản lý, nhân viên | Xem doanh thu và số đơn theo ca |

## 4. Use case chi tiết

### UC-01: Đăng nhập hệ thống

| Mục | Nội dung |
|---|---|
| Tác nhân | Quản lý, nhân viên |
| Tiền điều kiện | Database có tài khoản hợp lệ |
| Luồng chính | 1. Mở phần mềm. 2. Nhập username/password. 3. Bấm `Login`. |
| Kết quả mong đợi | Đăng nhập thành công, mở màn hình chính, hiển thị tên người dùng và vai trò |
| Luồng lỗi | Sai tài khoản hoặc mật khẩu |
| Kết quả lỗi | Hiển thị thông báo lỗi, không vào được màn hình chính |

### UC-02: Xem dashboard

| Mục | Nội dung |
|---|---|
| Tác nhân | Quản lý |
| Tiền điều kiện | Đăng nhập thành công, database có hóa đơn/sản phẩm |
| Luồng chính | 1. Chọn `Tổng Quan`. 2. Chọn khoảng ngày. 3. Bấm lọc hoặc chọn nhanh hôm nay/7 ngày/30 ngày. |
| Kết quả mong đợi | Dashboard hiển thị đúng tổng doanh thu, đơn hàng, lợi nhuận, tồn kho, biểu đồ |
| Luồng lỗi | Khoảng ngày không có hóa đơn |
| Kết quả lỗi | Doanh thu và đơn hàng bằng 0, không crash |

### UC-03: Xuất báo cáo Excel

| Mục | Nội dung |
|---|---|
| Tác nhân | Quản lý |
| Tiền điều kiện | Đã mở dashboard |
| Luồng chính | 1. Chọn khoảng ngày. 2. Bấm `Xuất báo cáo`. 3. Chọn nơi lưu file. |
| Kết quả mong đợi | File Excel có các sheet `ThongTin`, `TongQuan`, `TopSanPham`, `TonThap`, `SapHetHan`, `DoanhThuNgay`, `DanhMuc`, `Insight` |
| Kiểm tra thêm | Sheet `ThongTin` có tổng doanh thu, tổng đơn hàng, lợi nhuận, tồn kho và số dòng dữ liệu từng sheet |
| Luồng lỗi | Khoảng ngày không có dữ liệu |
| Kết quả lỗi | File vẫn xuất được và ghi rõ không có dữ liệu trong sheet liên quan |

### UC-04: Xem danh sách sản phẩm POS

| Mục | Nội dung |
|---|---|
| Tác nhân | Nhân viên |
| Tiền điều kiện | Database có sản phẩm |
| Luồng chính | 1. Chọn `Bán Hàng (POS)`. 2. Quan sát danh sách sản phẩm. |
| Kết quả mong đợi | Mỗi card có tên sản phẩm, mô tả/danh mục, giá, số lượng tồn và ảnh sản phẩm |
| Luồng lỗi | Sản phẩm chưa có ảnh |
| Kết quả lỗi | Hiển thị ảnh mặc định hoặc ảnh fallback theo tên sản phẩm |

### UC-05: Thêm sản phẩm vào giỏ hàng

| Mục | Nội dung |
|---|---|
| Tác nhân | Nhân viên |
| Tiền điều kiện | POS có sản phẩm còn hàng |
| Luồng chính | 1. Click sản phẩm. 2. Kiểm tra giỏ hàng. |
| Kết quả mong đợi | Sản phẩm được thêm vào giỏ với số lượng 1, đơn giá và thành tiền đúng |
| Luồng phụ | Click cùng sản phẩm nhiều lần |
| Kết quả phụ | Số lượng tăng, thành tiền cập nhật đúng |

### UC-06: Xóa sản phẩm khỏi giỏ hàng

| Mục | Nội dung |
|---|---|
| Tác nhân | Nhân viên |
| Tiền điều kiện | Giỏ hàng có ít nhất 1 sản phẩm |
| Luồng chính | 1. Chọn dòng sản phẩm trong giỏ. 2. Bấm `Xóa sản phẩm`. 3. Xác nhận xóa. |
| Kết quả mong đợi | Dòng sản phẩm bị xóa, tạm tính và tổng cộng được cập nhật |
| Luồng lỗi | Chưa chọn sản phẩm |
| Kết quả lỗi | Hiển thị thông báo yêu cầu chọn sản phẩm |

### UC-07: Áp dụng mã khuyến mãi

| Mục | Nội dung |
|---|---|
| Tác nhân | Nhân viên |
| Tiền điều kiện | Giỏ hàng có sản phẩm, database có mã khuyến mãi |
| Luồng chính | 1. Nhập mã khuyến mãi còn hiệu lực. 2. Bấm `Áp dụng`. |
| Kết quả mong đợi | Hệ thống hiển thị áp dụng thành công, giảm giá và tổng cộng cập nhật đúng |
| Luồng lỗi | Nhập mã sai, mã hết hạn hoặc mã chưa đến ngày bắt đầu |
| Kết quả lỗi | Thông báo mã không hợp lệ, không giảm giá |

### UC-08: Thanh toán hóa đơn

| Mục | Nội dung |
|---|---|
| Tác nhân | Nhân viên |
| Tiền điều kiện | Giỏ hàng có sản phẩm |
| Luồng chính | 1. Bấm `Thanh Toán`. 2. Nhập số tiền khách đưa. 3. Xác nhận thanh toán. |
| Kết quả mong đợi | Hóa đơn được lưu, chi tiết hóa đơn được lưu, tồn kho giảm đúng số lượng bán |
| Luồng lỗi | Giỏ hàng trống |
| Kết quả lỗi | Hiển thị thông báo không thể thanh toán khi giỏ hàng trống |

### UC-09: Tìm khách hàng khi thanh toán

| Mục | Nội dung |
|---|---|
| Tác nhân | Nhân viên |
| Tiền điều kiện | Có dữ liệu khách hàng trong database |
| Luồng chính | 1. Nhập số điện thoại khách hàng đã đăng ký. 2. Bấm thanh toán. |
| Kết quả mong đợi | Hệ thống nhận diện khách hàng, thông báo điểm được cộng |
| Luồng phụ | Số điện thoại chưa đăng ký |
| Kết quả phụ | Hỏi có muốn đăng ký khách hàng mới không |

### UC-10: Thêm/sửa/xóa sản phẩm

| Mục | Nội dung |
|---|---|
| Tác nhân | Quản lý |
| Tiền điều kiện | Đăng nhập bằng tài khoản quản lý |
| Luồng chính | 1. Mở `Sản Phẩm`. 2. Thêm sản phẩm mới. 3. Sửa thông tin. 4. Xóa sản phẩm. |
| Kết quả mong đợi | Danh sách sản phẩm cập nhật đúng, database thay đổi đúng |
| Luồng lỗi | Nhập thiếu tên, giá sai định dạng, tồn kho sai định dạng |
| Kết quả lỗi | Hiển thị thông báo validate, không lưu dữ liệu sai |

### UC-11: Chọn ảnh sản phẩm

| Mục | Nội dung |
|---|---|
| Tác nhân | Quản lý |
| Tiền điều kiện | Đang thêm hoặc sửa sản phẩm |
| Luồng chính | 1. Bấm chọn ảnh. 2. Chọn file `.jpg`, `.png`, `.bmp`. 3. Lưu sản phẩm. |
| Kết quả mong đợi | Ảnh được copy vào thư mục `Resources`, cột `HinhAnh` lưu tên file, POS hiển thị ảnh mới |
| Luồng lỗi | File ảnh không tồn tại hoặc không đọc được |
| Kết quả lỗi | Hiển thị thông báo không thể lưu ảnh |

### UC-12: Quản lý kho

| Mục | Nội dung |
|---|---|
| Tác nhân | Quản lý |
| Tiền điều kiện | Có danh sách sản phẩm và tồn kho |
| Luồng chính | 1. Mở `Quản Lý Kho`. 2. Xem số lượng tồn. 3. Tìm kiếm sản phẩm. |
| Kết quả mong đợi | Hiển thị đúng tồn kho, cảnh báo sản phẩm tồn thấp |
| Kiểm tra thêm | Sau khi bán hàng, tồn kho sản phẩm đã bán giảm đúng |

### UC-13: Quản lý nhà cung cấp

| Mục | Nội dung |
|---|---|
| Tác nhân | Quản lý |
| Tiền điều kiện | Đăng nhập thành công |
| Luồng chính | 1. Mở `Nhà Cung Cấp`. 2. Thêm nhà cung cấp. 3. Sửa thông tin. 4. Xóa nhà cung cấp. |
| Kết quả mong đợi | Dữ liệu nhà cung cấp được lưu, cập nhật và xóa đúng |
| Luồng lỗi | Thiếu tên hoặc số điện thoại không hợp lệ |
| Kết quả lỗi | Không lưu dữ liệu sai |

### UC-14: Quản lý khách hàng

| Mục | Nội dung |
|---|---|
| Tác nhân | Quản lý, nhân viên |
| Tiền điều kiện | Đăng nhập thành công |
| Luồng chính | 1. Thêm khách hàng mới. 2. Tìm theo số điện thoại. 3. Kiểm tra điểm tích lũy sau thanh toán. |
| Kết quả mong đợi | Khách hàng được lưu, điểm tích lũy tăng sau hóa đơn |
| Luồng lỗi | Trùng số điện thoại |
| Kết quả lỗi | Hệ thống không tạo khách hàng trùng hoặc báo lỗi phù hợp |

### UC-15: Quản lý mã khuyến mãi

| Mục | Nội dung |
|---|---|
| Tác nhân | Quản lý |
| Tiền điều kiện | Đăng nhập bằng tài khoản quản lý |
| Luồng chính | 1. Mở `Giá _ Khuyến Mãi`. 2. Thêm mã. 3. Sửa mã. 4. Xóa mã. |
| Kết quả mong đợi | Mã hiển thị đúng trạng thái: đang hiệu lực, sắp hiệu lực, đã kết thúc |
| Luồng lỗi | Phần trăm giảm không hợp lệ hoặc ngày kết thúc trước ngày bắt đầu |
| Kết quả lỗi | Hiển thị thông báo validate |

### UC-16: Xem lịch sử hóa đơn

| Mục | Nội dung |
|---|---|
| Tác nhân | Quản lý, nhân viên |
| Tiền điều kiện | Đã có hóa đơn phát sinh |
| Luồng chính | 1. Mở `Lịch Sử Hóa Đơn`. 2. Tìm kiếm/lọc hóa đơn. 3. Xem chi tiết. |
| Kết quả mong đợi | Hóa đơn hiển thị đúng ngày, tổng tiền, sản phẩm, khách hàng nếu có |

### UC-17: Quản lý nhân viên

| Mục | Nội dung |
|---|---|
| Tác nhân | Quản lý |
| Tiền điều kiện | Đăng nhập bằng tài khoản quản lý |
| Luồng chính | 1. Mở `Quản Lý Nhân Viên`. 2. Thêm nhân viên. 3. Sửa thông tin. 4. Xóa nhân viên. |
| Kết quả mong đợi | Nhân viên và tài khoản liên quan được lưu đúng |
| Luồng lỗi | Email sai định dạng, thiếu mật khẩu, xác nhận mật khẩu không khớp |
| Kết quả lỗi | Không lưu dữ liệu sai |

### UC-18: Phân công công việc

| Mục | Nội dung |
|---|---|
| Tác nhân | Quản lý |
| Tiền điều kiện | Có nhân viên và ca làm |
| Luồng chính | 1. Mở màn hình phân công. 2. Chọn nhân viên/ngày/ca. 3. Giao công việc. |
| Kết quả mong đợi | Công việc được lưu đúng nhân viên, đúng ngày và đúng ca |
| Luồng lỗi | Trùng công việc hoặc thiếu thông tin bắt buộc |
| Kết quả lỗi | Hiển thị cảnh báo, không lưu bản ghi sai |

### UC-19: Chỉnh sửa lịch làm

| Mục | Nội dung |
|---|---|
| Tác nhân | Quản lý |
| Tiền điều kiện | Có lịch làm của nhân viên |
| Luồng chính | 1. Chọn nhân viên và ngày làm. 2. Chỉnh ca hoặc chọn ngày nghỉ/tăng ca. 3. Lưu. |
| Kết quả mong đợi | Lịch làm cập nhật đúng, dữ liệu hiển thị lại đúng sau khi lưu |

### UC-20: Báo cáo ca làm

| Mục | Nội dung |
|---|---|
| Tác nhân | Quản lý, nhân viên |
| Tiền điều kiện | Có hóa đơn phát sinh trong ca |
| Luồng chính | 1. Mở báo cáo ca. 2. Chọn ca hoặc ngày. 3. Xem số đơn, doanh thu, sản phẩm bán. |
| Kết quả mong đợi | Số liệu ca làm khớp với hóa đơn trong database |

## 5. Bộ test case rút gọn để chạy kiểm thử

| TC | Chức năng | Dữ liệu nhập | Bước kiểm thử | Kết quả mong đợi |
|---|---|---|---|---|
| TC-01 | Đăng nhập đúng | Username/password hợp lệ | Nhập thông tin và bấm Login | Vào màn hình chính |
| TC-02 | Đăng nhập sai | Sai mật khẩu | Nhập thông tin và bấm Login | Báo lỗi, không đăng nhập |
| TC-03 | POS hiển thị ảnh | Sản phẩm có `HinhAnh` | Mở POS | Card sản phẩm có ảnh |
| TC-04 | Thêm hàng vào giỏ | Coca Cola | Click Coca Cola | Giỏ hàng có Coca Cola, SL = 1 |
| TC-05 | Tăng số lượng | Coca Cola | Click Coca Cola 2 lần | SL = 2, thành tiền nhân đôi |
| TC-06 | Xóa hàng khỏi giỏ | Dòng sản phẩm đã chọn | Bấm Xóa sản phẩm | Dòng bị xóa, tổng tiền cập nhật |
| TC-07 | Mã giảm giá đúng | Mã còn hiệu lực | Nhập mã và bấm Áp dụng | Tổng cộng giảm đúng phần trăm |
| TC-08 | Mã giảm giá sai | `SAI123` | Nhập mã và bấm Áp dụng | Báo mã không hợp lệ |
| TC-09 | Thanh toán không khách hàng | Giỏ hàng có sản phẩm | Bấm Thanh toán và xác nhận | Tạo hóa đơn, trừ tồn kho |
| TC-10 | Thanh toán có khách hàng | SĐT đã đăng ký | Nhập SĐT, thanh toán | Hóa đơn gắn khách hàng, cộng điểm |
| TC-11 | Xuất báo cáo có dữ liệu | Khoảng ngày có hóa đơn | Bấm xuất Excel | File có dữ liệu ở các sheet |
| TC-12 | Xuất báo cáo không dữ liệu | Ngày không có hóa đơn | Bấm xuất Excel | File vẫn tạo, sheet ghi không có dữ liệu |
| TC-13 | Thêm sản phẩm thiếu tên | Tên rỗng | Bấm lưu | Báo thiếu dữ liệu |
| TC-14 | Thêm sản phẩm giá sai | Giá = `abc` | Bấm lưu | Báo giá không hợp lệ |
| TC-15 | Chọn ảnh sản phẩm | File PNG hợp lệ | Chọn ảnh và lưu | Ảnh được lưu vào Resources |
| TC-16 | Xóa sản phẩm | Sản phẩm đang tồn tại | Bấm xóa và xác nhận | Sản phẩm biến mất khỏi danh sách |
| TC-17 | Thêm khuyến mãi | Mã mới, ngày hợp lệ | Bấm thêm/lưu | Mã xuất hiện trong danh sách |
| TC-18 | Khuyến mãi ngày sai | Ngày kết thúc trước ngày bắt đầu | Bấm lưu | Báo lỗi validate |
| TC-19 | Thêm khách hàng | SĐT mới | Lưu khách hàng | Khách hàng xuất hiện trong danh sách |
| TC-20 | Tìm hóa đơn | Mã hóa đơn/ngày | Tìm kiếm trong lịch sử hóa đơn | Hiển thị đúng hóa đơn |

## 6. Tiêu chí đạt kiểm thử

- Không crash khi thao tác các luồng chính.
- Dữ liệu lưu xuống database đúng.
- Các số liệu dashboard, báo cáo và lịch sử hóa đơn khớp với database.
- File Excel xuất ra có dữ liệu hoặc thông báo rõ khi không có dữ liệu.
- POS hiển thị ảnh, giá, số lượng tồn và cập nhật giỏ hàng đúng.
- Các form nhập liệu có validate dữ liệu sai.
- Sau thanh toán, tồn kho giảm và hóa đơn được lưu đầy đủ.
