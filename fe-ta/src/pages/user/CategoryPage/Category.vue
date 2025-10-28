<script lang="ts" setup>
import { onMounted, ref, watch } from "vue";
import { useRoute } from "vue-router";
import HeroSection from "../HomePage/components/HeroSection.vue";
import { useCategoryStore } from "@/store/categoryStore";
import { storeToRefs } from "pinia"; // Dùng để reactive store
import ProductHot from "../HomePage/components/ProductHot.vue";
import ProductList from "./components/ProductList.vue";

const route = useRoute();
const categoryStore = useCategoryStore();
const { selectedCategory, categoriesParent } = storeToRefs(categoryStore); //  Reactive refs

const updateCategoryBySlug = async (slug?: string) => {
  if (!slug) {
    categoryStore.selectedCategory = null;
    return;
  }

  // Nếu chưa có danh sách, load trước
  if (categoriesParent.value.length === 0) {
    await categoryStore.getAllCategoriesWithDetails();
  }

  // Tìm toàn bộ category (cha và con)
  const allCategories = [
    ...categoriesParent.value,
    ...categoriesParent.value.flatMap((c) => c.children || []),
  ];

  categoryStore.selectedCategory = allCategories.find((c) => c.slug === slug) || null;
};

const logos = ref<string[]>([]);

//Load lần đầu
onMounted(async () => {
  updateCategoryBySlug(route.params.slug as string);
});

// Khi slug thay đổi, tự cập nhật
watch(
  () => route.params.slug,
  async (newSlug, oldSlug) => {
    console.log("Slug changed:", oldSlug, "=>", newSlug);
    await updateCategoryBySlug(newSlug as string);
    window.scrollTo({ top: 0, behavior: "smooth" });
  }
);

watch(
  () => selectedCategory.value,
  (newCategory) => {
    if (newCategory?.children?.length) {
      logos.value = newCategory.children
        .map((child) => child.partner?.logoUrl)
        .filter((url): url is string => !!url); // chỉ lấy logo hợp lệ
    } else {
      logos.value = [];
    }
  },
  { immediate: true }
);
</script>

<template>
  <div>
    <div class="hero-section-category">
      <HeroSection
        :key="selectedCategory?.slug"
        :page-hero="selectedCategory?.slug || 'default-category'"
      />
    </div>

    <section class="category-page">
      <div v-if="categoryStore.loading">
        <el-skeleton animated :rows="6" style="width: 100%; height: 300px" />
      </div>
      <h2 v-if="selectedCategory">{{ selectedCategory.name }}</h2>
      <div v-if="logos.length" class="partner-logos">
        <div v-for="(url, index) in logos" :key="index" class="partner-item">
          <img :src="url" alt="Partner logo" class="partner-logo" />
        </div>
      </div>

      <div v-if="selectedCategory">
        <!-- Danh mục con -->
        <div v-if="selectedCategory.children?.length">
          <!-- <h3>Danh mục con</h3> -->
          <div
            v-for="child in selectedCategory.children"
            :key="child.id"
            class="child-category"
          >
            <!-- Hiển thị sản phẩm nổi bật -->
            <ProductHot :category-id="child.id" />
          </div>
        </div>

        <!-- Sản phẩm trong danh mục -->
        <div v-if="selectedCategory.products?.length">
          <h3>Sản phẩm trong danh mục</h3>
          <ProductList :products="selectedCategory.products" />
        </div>

        <!-- Không có gì -->
        <div
          v-if="
            !selectedCategory.products?.length &&
            !selectedCategory.children?.length
          "
        >
          <p>Không có danh mục con hoặc sản phẩm nào trong danh mục này.</p>
        </div>
      </div>

      <div v-else>
        <h2>Danh mục không tìm thấy</h2>
        <p>Vui lòng kiểm tra lại slug hoặc quay về trang chủ.</p>
      </div>
    </section>
  </div>
</template>

<style scoped>
.hero-section-category {
  width: 100%;
  min-height: 100%;
}

.category-page {
  padding: 2rem 6rem;
}
.partner-logos {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  gap: 2rem;
  margin-bottom: 2rem;
}

.partner-logo {
  width: 140px;
  height: 140px;
  object-fit: contain;
  border-radius: 1rem;
  transition: transform 0.3s ease;
}

.partner-logo:hover {
  transform: scale(1.08);
}
</style>
