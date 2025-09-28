# k58ktp_baitapweb
# k58ktp - Môn: Phát triển ứng dụng trên nền web #

## BÀI TẬP VỀ NHÀ 01: ##

### TẠO SOLUTION GỒM CÁC PROJECT SAU: ###
1. DLL đa năng, keyword: c# window library -> **Class Library (.NET Framework)** bắt buộc sử dụng **.NET Framework 2.0**: giải bài toán bất kỳ, độc lạ càng tốt, phải có dấu ấn cá nhân trong kết quả, biên dịch ra DLL. DLL độc lập vì nó ko nhập, ko xuất, nó nhận input truyền vào thuộc tính của nó, và trả về dữ liệu thông qua thuộc tính khác, hoặc thông qua giá trị trả về của hàm. Nó độc lập thì sẽ sử dụng được trên app dạng console (giao diện dòng lệnh - đen sì), cũng sử dụng được trên app desktop (dạng cửa sổ), và cũng sử dụng được trên web form (web chạy qua iis).
2. Console app, bắt buộc sử dụng **.NET Framework 2.0**, sử dụng được DLL trên: nhập được input, gọi DLL, hiển thị kết quả, phải có dấu án cá nhân. keyword: c# window Console => **Console App (.NET Framework)**, biên dịch ra EXE
3. Windows Form Application, bắt buộc sử dụng **.NET Framework 2.0****, sử dụng được DLL đa năng trên, kéo các control vào để có thể lấy đc input, gọi DLL truyền input để lấy đc kq, hiển thị kq ra window form, phải có dấu án cá nhân; keyword: c# window Desktop => **Windows Form Application (.NET Framework)**, biên dịch ra EXE
4. Web đơn giản, bắt buộc sử dụng **.NET Framework 2.0**, sử dụng web server là IIS, dùng file hosts để tự tạo domain, gắn domain này vào iis, file index.html có sử dụng html css js để xây dựng giao diện nhập được các input cho bài toán, dùng mã js để tiền xử lý dữ liệu, js để gửi lên backend. backend là api.aspx, trong code của api.aspx.cs thì lấy được các input mà js gửi lên, rồi sử dụng được DLL đa năng trên. kết quả gửi lại json cho client, js phía client sẽ nhận được json này hậu xử lý để thay đổi giao diện theo dữ liệu nhận dược, phải có dấu án cá nhân. keyword: c# window web => **ASP.NET Web Application (.NET Framework)** + tham khảo link chatgpt thầy gửi. project web này biên dịch ra DLL, phải kết hợp với IIS mới chạy được.

#### BÀI LÀM ####
1.MultiToolLib (DLL đa năng)
Loại: Class Library (.NET Framework 2.0)
Đây là thư viện chứa "thuật toán độc lạ có dấu ấn cá nhân".
Input → set vào property.
Output → đọc từ property khác hoặc từ kết quả hàm.
DLL này phải hoàn toàn độc lập: không nhập/xuất ra Console, không gắn với Form.

<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/f847a481-633f-41fd-8000-4cb96891829a" />

2.PuzzleConsole (Console app)
Loại: Console App (.NET Framework 2.0)
Nhập input từ người dùng (chuỗi hoặc số).
Gọi DLL (MultiToolLib).
Hiển thị kết quả ra màn hình đen.

<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/ebfbaf18-a2df-46d1-9a1e-33d864cb9f07" />

3.PuzzleDesktop (Windows Forms Application)
Loại: Windows Forms (.NET Framework 2.0)
Form có TextBox để nhập input, Button để xử lý, Label/TextBox multiline để hiển thị output.
Gọi DLL → hiện kết quả trên Form.


4.PuzzleWeb (ASP.NET WebForms)
Loại: ASP.NET Web Application (.NET Framework 2.0)
Tạo index.html với HTML + CSS + JS.
Người dùng nhập input → JS gửi POST request tới api.aspx.
api.aspx.cs gọi DLL → trả JSON.
JS nhận JSON → hiển thị kết quả đẹp trên giao diện web.

<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/3f1aeaa9-3864-4db2-a755-feb27b239a5a" />
