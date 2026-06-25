# Báo cáo kiểm thử phần mềm Cửa Hàng Tiện Lợi

Ngày kiểm thử: 24/06/2026  
Môi trường: Windows, SQL Server `localhost`, database `CUA_HANG_TIEN_LOI`  
Branch: `brand_thuan`

## 1. Phạm vi đã kiểm thử

- Build solution `CUA_HANG_TIEN_LOI.slnx`.
- Kiểm tra kết nối database thật.
- Kiểm tra dữ liệu nền: sản phẩm, hóa đơn, khách hàng, khuyến mãi, nhân viên.
- Smoke test các service chính trong `demo.BLL`.
- Kiểm tra xuất báo cáo Excel dashboard.
- Kiểm tra ảnh sản phẩm trong POS.
- Kiểm tra tính khớp giữa hóa đơn và chi tiết hóa đơn.

## 2. Kết quả tổng quan

| Nhóm kiểm thử | Kết quả |
|---|---|
| Build Debug | Đạt |
| Kết nối database thật | Đạt |
| Đăng nhập đúng/sai | Đạt |
| Dữ liệu sản phẩm | Đạt |
| Ảnh sản phẩm POS | Đạt |
| Dashboard | Đạt |
| Xuất báo cáo Excel | Đạt |
| Kho / tìm sản phẩm | Đạt |
| Khuyến mãi | Đạt một phần |
| Khách hàng | Đạt |
| Lịch sử hóa đơn | Đạt |
| Nhân viên | Đạt |

Tổng smoke test tự động:

| Trạng thái | Số lượng |
|---|---:|
| Passed | 15 |
| Failed | 0 |
| Skipped | 1 |

## 3. Kết quả build

Build bằng lệnh:

```powershell
MSBuild.exe CUA_HANG_TIEN_LOI.slnx /m /v:minimal /p:Configuration=Debug
```

Kết quả: build thành công.

Ghi chú: vẫn còn warning `MSB3277` về xung đột version `System.Memory`. Warning này không chặn build và app vẫn sinh `demo.exe`, nhưng nên xử lý sau để project sạch hơn.

## 4. Dữ liệu database kiểm tra được

| Dữ liệu | Số lượng |
|---|---:|
| Sản phẩm | 10 |
| Hóa đơn | 15 |
| Khách hàng | 11 |
| Khuyến mãi | 10 |

Tài khoản test hợp lệ:

| Username | Password | Vai trò |
|---|---|---|
| `user1` | `123` | Quản lý |
| `user2` | `123` | Nhân viên |

## 5. Chi tiết test case đã chạy

| TC | Nội dung | Kết quả |
|---|---|---|
| TC-01 | Đăng nhập đúng `user1/123` | Pass |
| TC-02 | Đăng nhập sai mật khẩu | Pass |
| TC-03 | Database có 10 sản phẩm mẫu | Pass |
| TC-04 | 10 sản phẩm đều có `HinhAnh` và file ảnh tồn tại | Pass |
| TC-05 | `InventoryService` lấy tổng mặt hàng và tồn kho | Pass |
| TC-06 | Tìm sản phẩm `Coca` trong kho | Pass |
| TC-07 | Dashboard khoảng 01/04/2026 - 24/06/2026 có dữ liệu | Pass |
| TC-08 | Xuất báo cáo Excel dashboard | Pass |
| TC-09 | Lấy danh sách khuyến mãi | Pass |
| TC-10 | Validate khuyến mãi bắt lỗi ngày kết thúc trước ngày bắt đầu | Pass |
| TC-11 | Áp dụng mã khuyến mãi đang hiệu lực hôm nay | Skip |
| TC-12 | Lấy danh sách khách hàng | Pass |
| TC-13 | Lịch sử hóa đơn có dữ liệu | Pass |
| TC-14 | Lấy danh sách nhân viên và dashboard nhân viên | Pass |
| TC-15 | Kiểm tra tổng tiền hóa đơn khớp tổng chi tiết hóa đơn | Pass |

Lý do skip TC-11: ngày kiểm thử là 24/06/2026, database không có mã khuyến mãi nào đang hiệu lực trong ngày này. Các mã hiện tại đều đã hết hạn hoặc chưa đến ngày áp dụng.

## 6. Lỗi phát hiện

### BUG-01: Tổng tiền hóa đơn không khớp tổng chi tiết hóa đơn

Mức độ: Cao  
Khu vực ảnh hưởng: lịch sử hóa đơn, dashboard, báo cáo doanh thu, đối soát bán hàng.
Trạng thái: Đã sửa và kiểm thử lại đạt.

Mô tả:

Một số bản ghi trong bảng `HOA_DON` có `TongTien` khác với tổng `ThanhTien` trong bảng `CHI_TIET_HOA_DON`.

Các hóa đơn lệch trước khi sửa:

| Mã hóa đơn | Ngày lập | Tổng tiền hóa đơn | Tổng chi tiết | Chênh lệch |
|---:|---|---:|---:|---:|
| 4 | 2026-03-04 00:00:00 | 50.000 | 30.000 | 20.000 |
| 5 | 2026-03-05 00:00:00 | 22.000 | 16.000 | 6.000 |
| 7 | 2026-03-07 00:00:00 | 26.000 | 24.000 | 2.000 |
| 8 | 2026-03-08 00:00:00 | 34.000 | 28.000 | 6.000 |
| 9 | 2026-03-09 00:00:00 | 41.000 | 40.000 | 1.000 |
| 10 | 2026-03-10 00:00:00 | 12.000 | 11.000 | 1.000 |
| 13 | 2026-05-23 03:26:13 | 36.000 | 40.000 | -4.000 |
| 14 | 2026-05-23 10:12:19 | 80.000 | 100.000 | -20.000 |

Rủi ro:

- Dashboard đang tính doanh thu từ `HOA_DON.TongTien`.
- Top sản phẩm và doanh thu danh mục đang tính từ `CHI_TIET_HOA_DON.ThanhTien`.
- Khi hai nguồn không khớp, báo cáo có thể bị lệch số liệu.

Nguyên nhân có khả năng:

- Dữ liệu seed hóa đơn chưa nhất quán.
- Hóa đơn có giảm giá nhưng database không lưu cột giảm giá riêng, nên `TongTien` sau giảm giá có thể khác tổng chi tiết trước giảm giá.
- POS hiện lưu `HOA_DON.TongTien` theo tổng cuối cùng, còn `CHI_TIET_HOA_DON.ThanhTien` theo từng sản phẩm.

Khuyến nghị sửa:

- Đã cập nhật dữ liệu seed để `HOA_DON.TongTien` khớp tổng `CHI_TIET_HOA_DON.ThanhTien`.
- Đã cập nhật logic POS: khi áp dụng giảm giá toàn đơn, tiền giảm được phân bổ vào từng dòng chi tiết hóa đơn để tổng chi tiết luôn bằng tổng tiền hóa đơn.
- Về lâu dài, nếu cần báo cáo rõ phần giảm giá, nên thêm cột `TienGiamGia` hoặc `PhanTramGiam` cho hóa đơn.

## 7. Các điểm đạt

- App build được.
- Database thật kết nối được.
- Đăng nhập đúng/sai hoạt động.
- 10 sản phẩm có ảnh và POS có thể load ảnh.
- Dashboard lấy được dữ liệu.
- Xuất Excel tạo được file có dữ liệu.
- Tổng tiền hóa đơn khớp tổng chi tiết hóa đơn.
- Danh sách khuyến mãi, khách hàng, nhân viên đọc được từ database.
- Validate form khuyến mãi bắt được ngày sai.

## 8. Các phần chưa kiểm thử tự động

- Click thủ công toàn bộ UI WinForms.
- Thanh toán POS bằng thao tác thật trên giao diện.
- Thêm/sửa/xóa sản phẩm qua form thật.
- Thêm/sửa/xóa nhân viên qua form thật.
- Phân công công việc và chỉnh sửa lịch làm bằng UI.

Các phần này cần kiểm thử thủ công trên Visual Studio hoặc cần thêm test automation chuyên cho WinForms.

## 9. Kết luận

Phần mềm chạy được các luồng chính ở mức service/database. BUG-01 về tổng tiền hóa đơn đã được sửa và smoke test lại không còn test case fail. Các phần UI WinForms vẫn nên kiểm thử thủ công thêm trước khi demo chính thức.
