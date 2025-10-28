<script lang="ts" setup>
import HeroSection from "@/pages/user/HomePage/components/HeroSection.vue";
import CategoryPartner from "./components/CategoryPartner.vue";
import Promo from "./components/Promo.vue";
import HomeTab from "./components/HomeTab.vue";
</script>

<template>
  <div class="hero-section">
    <HeroSection page-hero="home" />
  </div>
  <div class="category-partner">
    <CategoryPartner />
  </div>
  <div class="promo">
    <Promo />
  </div>
  <div class="home-tab">
    <HomeTab />
  </div>

</template>

<style lang="css" scoped>
.hero-section {
  width: 100%;
  height: 70vh;
}
.category-partner {
  width: 100%;
  min-height: 50vh;
  padding-top: 2rem;
  padding-bottom: 4rem;
}

.promo {
  width: 100%;
  min-height: 50vh;
  
}

.home-tab {
  width: 95%;
  min-height: 50vh;
  padding: 4rem 2rem;
}

.product-hot {
  width: 95%;
  min-height: 50vh;
  padding: 1rem 2rem;
}
</style>
