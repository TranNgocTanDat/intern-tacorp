<script lang="ts" setup>
import { ref, onMounted } from "vue";
import { useCategoryStore } from "@/store/categoryStore";
import { storeToRefs } from "pinia";
import { useAppStore } from "@/store/appStore";

// Hover control
const activeDropdown = ref<string | null>(null);
let timeout: number | null = null;

const showDropdown = (slug: string) => {
  if (timeout) clearTimeout(timeout);
  activeDropdown.value = slug;
};

const hideDropdown = () => {
  timeout = window.setTimeout(() => {
    activeDropdown.value = null;
  }, 150);
};

// Store
const categoryStore = useCategoryStore();
const { categoriesParent, loading } = storeToRefs(categoryStore);

onMounted(async () => {
  if (categoriesParent.value.length === 0) {
    await categoryStore.getAllCategoriesWithDetails();
  }
});
const reloadPage = (url: string) => {
  window.location.href = url; // reload thật
};

const appStore = useAppStore();
</script>

<template>
  <el-header class="header-bar">
    <div class="logo" @click.prevent="appStore.goHome">TechStore</div>

    <div class="menu-wrapper">
      <template v-if="!loading">
        <div
          v-for="category in categoriesParent"
          :key="category.slug"
          class="nav-item"
          @mouseenter="showDropdown(category.slug)"
          @mouseleave="hideDropdown"
        >
          <router-link
            :to="`/category/${category.slug}`"
            @click.prevent="reloadPage(`/category/${category.slug}`)"
          >
            {{ category.name }}
          </router-link>

          <!-- Dropdown con (không transition) -->
          <div
            v-if="activeDropdown === category.slug"
            class="submenu-items"
            @mouseenter="showDropdown(category.slug)"
            @mouseleave="hideDropdown"
          >
            <div
              class="items-child"
              v-for="(brand, i) in category.children"
              :key="i"
            >
              <router-link
                :to="`/category/${brand.slug}`"
                @click.prevent="reloadPage(`/category/${brand.slug}`)"
              >
                {{ brand.name }}
              </router-link>
            </div>
          </div>
        </div>
      </template>

      <el-skeleton v-else animated :rows="1" style="width: 300px" />
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
  height: 100%;
  align-items: center;
  position: relative;
  background: #fff;
}

.logo {
  align-self: center;
  display: flex;
  height: 28px;
  margin-left: 36px;
  font-weight: bold;
  font-size: 22px;
}

.logo:hover {
  cursor: pointer;
}

.menu-wrapper {
  height: 100%;
  display: flex;
  margin: 0 auto;
  align-items: center;
  gap: 48px;
}

.nav-item {
  font-weight: 500;
}

/* Dropdown menu cố định kích thước, hiển thị ngay */
.submenu-items {
  position: fixed;
  top: 66px;
  left: 0;
  width: 100%;
  background-color: white;
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 30px;
  padding: 20px 0;
  z-index: 1000;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.05);
  border-top: 1px solid #eee;
}

/* Giữ nguyên text */
.items-child a {
  transition: color 0.2s;
}
</style>
