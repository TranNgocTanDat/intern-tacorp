<script lang="ts" setup>
// ...existing code...
import { useRoute } from "vue-router";
import HeroSection from "../HomePage/components/HeroSection.vue";
import { categories } from "@/data/categories";

const route = useRoute();
const slug = route.params.slug as string | undefined;
const category = slug ? categories.find((c) => c.slug === slug) : undefined;
</script>

<template>
  <div>
    <div class="hero-section-category">
      <HeroSection />
    </div>

    <section class="category-page">
      <div v-if="category">
        <h2>{{ category.name }}</h2>
        <p>Slug: {{ category.slug }}</p>
        <h3>Thương hiệu</h3>
        <ul>
          <li v-for="brand in category.brands" :key="brand">{{ brand }}</li>
        </ul>
      </div>
      <div v-else>
        <h2>Danh mục không tìm thấy</h2>
        <p>Vui lòng kiểm tra lại slug hoặc quay về trang chủ.</p>
      </div>
    </section>
  </div>
</template>

<style scoped>

.category-page {
  padding: 2rem;
}
</style>
