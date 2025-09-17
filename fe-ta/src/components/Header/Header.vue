<script lang="ts" setup>
import { ref } from "vue";

const categories = [
  {
    name: "ĐIỆN THOẠI",
    slug: "dien-thoai",
    brands: [
      "Apple",
      "Samsung",
      "Xiaomi",
      "Oppo",
      "Vivo",
      "Realme",
      "OnePlus",
      "Asus",
      "Google Pixel",
      "Sony",
      "Huawei",
    ],
  },
  {
    name: "LAPTOP",
    slug: "laptop",
    brands: ["Dell", "HP", "Lenovo", "Asus", "MSI"],
  },
  {
    name: "PHỤ KIỆN",
    slug: "phu-kien",
    brands: ["Chuột", "Bàn phím", "Sạc dự phòng", "Ốp lưng"],
  },
  {
    name: "TV",
    slug: "tv",
    brands: ["Samsung", "LG", "Sony"],
  },
  {
    name: "TAI NGHE",
    slug: "tai-nghe",
    brands: ["Sony", "Apple", "JBL", "Anker"],
  },
  {
    name: "MÀN HÌNH",
    slug: "man-hinh",
    brands: ["LG", "Dell", "Samsung", "AOC"],
  },
];

const activeDropdown = ref<string | null>(null);

let timeout: number | null = null;

const showDropdown = (slug: string) => {
  if (timeout) clearTimeout(timeout);
  activeDropdown.value = slug;
};

const hideDropdown = () => {
  timeout = window.setTimeout(() => {
    activeDropdown.value = null;
  }, 200); // 200ms delay
};
</script>

<template>
  <el-header class="header-bar">
    <div class="logo">TechStore</div>

    <div class="menu-wrapper">
      <div
        v-for="category in categories"
        :key="category.slug"
        class="nav-item"
        @mouseenter="showDropdown(category.slug)"
        @mouseleave="hideDropdown"
      >
        {{ category.name }}

        <div
          v-show="activeDropdown === category.slug"
          class="submenu-items"
          @mouseenter="showDropdown(category.slug)"
          @mouseleave="hideDropdown"
        >
          <div
            class="items-child"
            v-for="(brand, i) in category.brands"
            :key="i"
          >
            <a
              :href="`/${category.slug}/${brand
                .toLowerCase()
                .replace(/\s/g, '-')}`"
            >
              {{ brand }}
            </a>
          </div>
        </div>
      </div>
    </div>

    <div class="search-bar">
      <el-input
        placeholder="Nhập tên sản phẩm cần tìm"
        prefix-icon="Search"
        size="default"
        style="width: 300px"
      />
    </div>
  </el-header>
</template>

<style scoped>
.header-bar {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 0 5rem;
  height: 70px;
  position: relative;
}

.logo {
  font-weight: bold;
  font-size: 22px;
}

.menu-wrapper {
  display: flex;
  align-items: center;
  gap: 20px;
}

.nav-item {
  position: relative;
  cursor: pointer;
  font-weight: 500;
  padding: 10px;
}

.nav-item:hover {
  color: #409eff;
}

.submenu-items {
  position: fixed;
  top: 70px;
  left: 0;
  width: 100%;
  max-width: 100%;
  height: 80px;
  background-color: white;
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  align-items: center;
  gap: 30px;
  padding: 20px 0;
  z-index: 1000;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.05);
  border-top: 1px solid #eee;
}

.items-child a {
  color: #333;
  margin: 0 40px;
  text-decoration: none;
  font-size: 14px;
}

.items-child a:hover {
  color: #409eff;
}
</style>
