<script lang="ts" setup>
import { computed } from "vue";
import {
  heroSections,
  heroSectionProducts,
} from "@/pages/user/HomePage/components/heroData";

const now = new Date();

// Lấy HeroSection đang publish
const heroSection = computed(() => {
  return heroSections.find(
    (hs) =>
      hs.isPublished &&
      new Date(hs.publishFrom) <= now &&
      (!hs.publishTo || new Date(hs.publishTo) >= now)
  );
});

// Lấy sản phẩm liên kết
const products = computed(() => {
  if (!heroSection.value) return [];
  return heroSectionProducts
    .filter((hp) => hp.HeroSectionId === heroSection.value!.ID)
    .sort((a, b) => a.orderIndex - b.orderIndex);
});
</script>

<template>
  <div class="hero-wrapper">
    <!-- Background (Image or Video) -->
    <div class="hero-background">
      <img
        v-if="heroSection?.heroMediaType === 'image'"
        :src="heroSection.HeroMediaUrl"
        alt="Hero Background"
        class="hero-media"
      />
      <video
        v-else-if="heroSection?.heroMediaType === 'video'"
        :src="heroSection.HeroMediaUrl"
        autoplay
        muted
        loop
        playsinline
        class="hero-media"
      ></video>

      <!-- Text overlay -->
      <div class="hero-text">
        <h2>{{ heroSection?.title }}</h2>
        <p>{{ heroSection?.ShortDescription }}</p>
      </div>
    </div>

    <!-- Product Carousel -->
    <el-carousel
      :interval="0"
      trigger="click"
      arrow="never"
      type="card"
      class="products-carousel"
    >
      <el-carousel-item width="300px" v-for="p in products" :key="p.ID">
        <div class="product-card">
          <img :src="`https://www.apple.com/newsroom/images/2025/09/apple-debuts-iphone-17/article/Apple-iPhone-17-hero-250909_inline.jpg.large_2x.jpg`" alt="Product" class="image-product" />
          <p>Iphone {{ p.productId }}</p>
        </div>
      </el-carousel-item>
    </el-carousel>
  </div>
</template>

<style scoped>
.hero-wrapper {
  position: relative;
  width: 100%;
  height: 100vh;
  overflow: hidden;
}

/* Hero background: full-screen image/video */
.hero-background {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  z-index: 1;
}

.hero-media {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

/* Text overlay */
.hero-text {
  position: absolute;
  bottom: 30px;
  left: 50px;
  z-index: 2;
  color: white;
  text-shadow: 1px 1px 5px rgba(0, 0, 0, 0.7);
}

/* Carousel products overlay */
.products-carousel {
  position: absolute;
  top: 30%;
  right: 10%;
  z-index: 3;
  width: 500px;
  padding: 0 20px;
  background: rgba(0, 0, 0, 0.2); /* optional */
  border-radius: 10px;
}

.products-carousel ::v-deep(.el-carousel__item) {
  display: flex;
  justify-content: center;
}

/* Product Card */
.product-card {
  display: flex;
  flex-direction: column;
  align-items: center;
  cursor: pointer;
  transition: transform 0.2s;
  padding: 10px;
  border-radius: 8px;
}


.product-card:hover {
  transform: scale(1.05);
}

.product-card img {
  width: 200px;
  height: 200px;
  background-size: contain;
  border-radius: 4px;
}
</style>
