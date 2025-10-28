<template>
  <footer class="footer-wrapper">
    <!-- Logo + tên -->
    <div class="footer-brand">
      <img src="" alt="TechShop Logo" class="footer-logo" />
      <h2 class="footer-name">TechShop</h2>
    </div>

    <div class="footer-container">
      <!-- Cột 1 -->
      <div class="footer-col">
        <h4 class="footer-title">VỀ TECHSHOP</h4>
        <ul>
          <li>Giới thiệu</li>
          <li>Tuyển dụng</li>
          <li>Chính sách đại lý</li>
          <li>Liên hệ</li>
          <li>Hệ thống cửa hàng</li>
        </ul>
      </div>

      <!-- Cột 2 -->
      <div class="footer-col">
        <h4 class="footer-title">HỖ TRỢ KHÁCH HÀNG</h4>
        <ul>
          <li>Hướng dẫn mua hàng</li>
          <li>Chính sách đổi trả</li>
          <li>Bảo hành & sửa chữa</li>
          <li>Thanh toán & giao hàng</li>
          <li>Câu hỏi thường gặp</li>
        </ul>
      </div>

      <!-- Cột 3 -->
      <div class="footer-col">
        <h4 class="footer-title">DANH MỤC SẢN PHẨM</h4>
        <ul>
          <li>Điện thoại</li>
          <li>Laptop</li>
          <li>Máy tính bảng</li>
          <li>Phụ kiện</li>
          <li>Âm thanh & Smart Home</li>
        </ul>
      </div>

      <!-- Cột 4 -->
      <div class="footer-col">
        <h4 class="footer-title">ĐĂNG KÝ NHẬN TIN</h4>
        <p>Nhận thông tin sản phẩm và khuyến mãi mới nhất</p>
        <el-input
          v-model="email"
          placeholder="Email của bạn"
          class="footer-input"
          clearable
        />
        <el-input
          v-model="phone"
          placeholder="Số điện thoại (tuỳ chọn)"
          class="footer-input"
          clearable
        />
        <el-button
          type="primary"
          class="footer-btn"
          color="#0071e3"
          round
          @click="handleSubmit"
        >
          ĐĂNG KÝ
        </el-button>
        
      </div>
    </div>

    <!-- Mạng xã hội -->
    <div class="social-icons">
      <el-icon><i class="fa-brands fa-facebook"></i></el-icon>
      <el-icon><i class="fa-brands fa-instagram"></i></el-icon>
      <el-icon><i class="fa-brands fa-tiktok"></i></el-icon>
      <el-icon><i class="fa-brands fa-youtube"></i></el-icon>
    </div>

    <div class="footer-bottom">
      <p>© 2025 TechShop Việt Nam. All rights reserved.</p>
    </div>
  </footer>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { ElMessage } from "element-plus";

const email = ref("");
const phone = ref("");

const handleSubmit = () => {
  if (!email.value) {
    ElMessage.warning("Vui lòng nhập email để đăng ký!");
    return;
  }
  ElMessage.success("Đăng ký nhận tin thành công!");
  email.value = "";
  phone.value = "";
};
</script>

<style scoped>
.footer-wrapper {
  background: #fff;
  color: #222;
  padding: 40px 80px;
  border-top: 1px solid #eee;
  display: flex;
  flex-direction: column;
  align-items: center;
}

.footer-brand {
  display: flex;
  align-items: center;
  gap: 12px;
  margin-bottom: 30px;
}

.footer-logo {
  width: 48px;
  height: 48px;
}

.footer-name {
  font-size: 1.5rem;
  font-weight: 700;
}

.footer-container {
  width: 100%;
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
  gap: 40px;
  margin-bottom: 20px;
}

.footer-col {
  flex: 1;
  min-width: 220px;
}

.footer-title {
  font-size: 14px;
  font-weight: 700;
  margin-bottom: 12px;
  letter-spacing: 1px;
}

ul {
  list-style: none;
  padding: 0;
  margin: 0;
}

ul li {
  margin-bottom: 8px;
  font-size: 14px;
  cursor: pointer;
  transition: color 0.2s;
}

ul li:hover {
  color: #0071e3;
}

.footer-input {
  margin-bottom: 10px;
}

.footer-btn {
  width: 100%;
  font-weight: 600;
  letter-spacing: 1px;
  color: white;
}

.privacy-text {
  font-size: 12px;
  margin-top: 8px;
  color: #555;
}

.privacy-text a {
  color: #0071e3;
  text-decoration: none;
}

.social-icons {
  text-align: center;
  font-size: 20px;
  margin: 20px 0;
}

.social-icons i {
  margin: 0 10px;
  cursor: pointer;
  transition: color 0.2s;
}

.social-icons i:hover {
  color: #0071e3;
}

.footer-bottom {
  text-align: center;
  font-size: 13px;
  color: #666;
}

/* Responsive */
@media (max-width: 900px) {
  .footer-container {
    flex-direction: column;
    align-items: center;
  }
  .footer-col {
    width: 100%;
    text-align: center;
  }
}
</style>
