<script lang="ts" setup>
import { ref } from "vue";
import type { LoginRequest } from "@/models/Authentication";
import router from "@/router/Router";
import auth from "@/services/auth";

const loginForm = ref<LoginRequest>({
  username: "",
  password: "",
});

const handleLogin = async () => {
  try {
    const response = await auth.login(loginForm.value);
    localStorage.setItem("token", response.accessToken);
    router.push("/dashboard");
  } catch (error) {
    console.error("Lỗi khi đăng nhập:", error);
    alert("Đăng nhập thất bại. Vui lòng kiểm tra lại thông tin.");
  }
}
</script>

<template>
  <div class="login-page">
    <div class="background-login">
      <img
        width="100%"
        height="100%"
        src="https://hoanghamobile.com/tin-tuc/wp-content/webp-express/webp-images/uploads/2024/08/background-cong-nghe-1.jpg.webp"
        alt="background"
      />
    </div>
    <div class="left-panel">
      <div class="overlay">
        <h1>Welcome Back</h1>
        <p>Quản trị nội dung và đơn hàng</p>
      </div>
    </div>
    <div class="right-panel">
      <form @submit.prevent="handleLogin" class="login-form">
        <h2>Đăng nhập Admin</h2>
        <div class="form-group">
          <label for="username">Tên đăng nhập</label>
          <input
            id="username"
            v-model="loginForm.username"
            type="text"
            required
            placeholder="Nhập tên đăng nhập"
          />
        </div>
        <div class="form-group">
          <label for="password">Mật khẩu</label>
          <input
            id="password"
            v-model="loginForm.password"
            type="password"
            required
            placeholder="Nhập mật khẩu"
          />
        </div>
        <button type="submit">Đăng nhập</button>
      </form>
    </div>
  </div>
</template>

<style lang="css" scoped>
.login-page {
  display: flex;
  justify-content: space-around;
  align-items: center;
  min-height: 100vh;
  background: linear-gradient(180deg, #f3f7ff 0%, #ffffff 100%);
  border-radius: 12px;
  overflow: hidden;
}
.background-login {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  overflow: hidden;
}
.left-panel {
  width: 40%;
  background: transparent;
  color: white;
  position: relative;
  padding: 2rem;
  z-index: 1;
}
.left-panel .overlay {
  text-align: center;
}
.illustration {
  width: 160px;
  height: auto;
  opacity: 0.95;
  margin-bottom: 1rem;
}
.left-panel h1 {
  font-size: 1.8rem;
  margin: 0.25rem 0;
}
.left-panel p {
  opacity: 0.95;
}
.right-panel {
  width: 40%;
  z-index: 1;

  padding: 3rem 2rem;
  /* background: white/ */
}
.login-form {
  width: 100%;
  max-width: 360px;
  background: rgba(255, 255, 255, 0); /* Nền mờ mờ như ảnh */
  border-radius: 16px;
  backdrop-filter: blur(3px); /* Làm mờ nền phía sau */
  box-shadow: 10px 10px 10px 1px rgba(31, 38, 135, 0.5); /* Shadow như trong ảnh */
  padding: 2rem;
  border: 1px solid rgba(255, 255, 255, 0.18); /* Viền nhẹ tạo chiều sâu */
}
.login-form h2 {
  margin-bottom: 1.2rem;
  color: #d3ddf3;
  text-align: center;
}
.form-group {
  margin-bottom: 1rem;
}
.form-group label {
  display: block;
  margin-bottom: 0.4rem;
  font-weight: 600;
  color: #ffffff;
}
.form-group input {
  width: 100%;
  padding: 0.6rem 0.8rem;
  border: 1px solid #e6e9ef;
  border-radius: 8px;
  font-size: 0.95rem;
  background: #fff;
}
button[type="submit"] {
  margin: auto 100px;
  width: 50%;
  padding: 0.75rem;
  background: rgba(4, 8, 26, 0.5); /* Nền mờ mờ như ảnh */
  color: #fff;
  border: 1px solid rgba(255, 255, 255, 0.18);
  border-radius: 10px;
  font-size: 1rem;
  font-weight: 700;
  cursor: pointer;
  box-shadow: 0 6px 20px rgba(79, 70, 229, 0.18);
}
button[type="submit"]:hover {
  transform: translateY(-1px);
}

@media (max-width: 900px) {
  .login-page {
    flex-direction: column;
  }
  .left-panel {
    flex: none;
    width: 100%;
    padding: 2rem 1.5rem;
  }
  .right-panel {
    padding: 2rem 1rem;
  }
  .illustration {
    width: 120px;
  }
}
</style>
