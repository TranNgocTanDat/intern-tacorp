<script lang="ts" setup>
import { ref, computed, watch } from "vue";
import { useHeroSectionStore } from "@/store/heroSectionStore";
import type { HeroSectionResponse } from "@/models/HeroSection";

// Props
interface PageHeroProps {
  pageHero: string;
}
const props = defineProps<PageHeroProps>();

// Store
const heroSectionStore = useHeroSectionStore();

// Local state
const heroSection = ref<HeroSectionResponse | null>(null);
const heroProducts = computed(() => heroSection.value?.heroProducts ?? []);

// Load hero sections from store
watch(
  () => heroSectionStore.sections,
  () => {
    updateHeroSection();
  },
  { immediate: true }
);

async function loadData() {
  await heroSectionStore.loadByPageHero(props.pageHero);
  updateHeroSection();
}

// Call loadData immediately
loadData();

// Function chọn section hợp lệ
function updateHeroSection() {
  const now = new Date().getTime();
  heroSection.value =
    heroSectionStore.sections.find((s) => {
      const start = new Date(s.publishFrom ?? "").getTime();
      const end = new Date(s.publishTo ?? "").getTime();
      return now >= start && now <= end;
    }) ?? null;
}
</script>

<template>
  <div class="hero-wrapper">
    <!-- Hero Background -->
    <div class="hero-background">
      <img
        v-if="heroSection?.heroMediaType === 'image'"
        :src="heroSection.heroMediaUrl"
        alt="Hero Background"
        class="hero-media"
      />
      <video
        v-else-if="heroSection?.heroMediaType === 'video'"
        :src="heroSection.heroMediaUrl"
        autoplay muted loop playsinline
        class="hero-media"
      ></video>

      <!-- Text Overlay -->
      <div class="hero-text">
        <h2>{{ heroSection?.title }}</h2>
        <p>{{ heroSection?.description }}</p>
      </div>
    </div>

    <!-- Product Carousel -->
    <el-carousel
      class="products-carousel"
      arrow="never"
      trigger="click"
      type="card"
      :interval="3000"
      :autoplay="true"
    >
      <el-carousel-item
        v-for="p in heroProducts.slice(0, 4)"
        :key="p.productId"
        width="300px"
      >
        <div class="product-card">
          <img
            :src="p.product?.mediaList?.find((m) => m.isPrimary)?.mediaFileUrl"
            alt="Product"
            class="image-product"
          />
          <p>{{ p.product?.productName }}</p>
        </div>
      </el-carousel-item>
    </el-carousel>
  </div>
</template>

<style scoped>
.hero-wrapper {
  position: relative;
  width: 100%;
  height: 70vh;
  overflow: hidden;
}

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
  background-size: contain;
}

.hero-text {
  position: absolute;
  bottom: 30px;
  left: 50px;
  z-index: 2;
  color: white;
  text-shadow: 2px 2px 5px rgba(0, 0, 0, 0.9);
}

.products-carousel {
  position: absolute;
  top: 30%;
  right: 10%;
  width: 500px;
  padding: 0 20px;
  z-index: 3;
  background: rgba(0, 0, 0, 0.2);
  border-radius: 10px;
}

.products-carousel ::v-deep(.el-carousel__item) {
  display: flex;
  justify-content: center;
}

.product-card {
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 10px;
  border-radius: 8px;
  cursor: pointer;
  transition: transform 0.3s ease, box-shadow 0.3s ease;
}

.product-card:hover {
  transform: scale(1.08) rotateZ(-1deg);
  box-shadow: 0 10px 20px rgba(0, 0, 0, 0.4);
}

.product-card p {
  text-align: center;
  color: white;
  text-transform: uppercase;
}

.image-product {
  width: 200px;
  height: 200px;
  object-fit: contain;
  border-radius: 4px;
}
</style>
