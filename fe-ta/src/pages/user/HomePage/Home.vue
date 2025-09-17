<script lang="ts" setup>
import HeroSection from "@/pages/user/HomePage/components/HeroSection.vue";
import ProductHot from "@/pages/user/HomePage/components/ProductHot.vue";
</script>

<template>
  <div class="hero-section">
    <HeroSection />
  </div>
  <div class="product-hot">
    <ProductHot />
  </div>
</template>

<style lang="css" scoped>
.hero-section {
  width: 100%;
  min-height: 100vh;
  /* background-image: url("https://png.pngtree.com/background/20240114/original/pngtree-gadgets-in-a-striking-3d-dim-environment-picture-image_7276667.jpg"); */
}

.product-hot {
  width: 95%;
  min-height: 50vh;
  padding: 2rem;
}
</style>
