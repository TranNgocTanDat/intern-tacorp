<script lang="ts" setup>
import { ref, onMounted } from "vue";
import { storeToRefs } from "pinia";
import { Carousel, Slide, type CarouselExposed } from "vue3-carousel";
import { usePartnerStore } from "@/store/partnerStore";

// Store
const partnerStore = usePartnerStore();
const { partners } = storeToRefs(partnerStore);

const carousel = ref<CarouselExposed>();

// Fetch partners
onMounted(() => {
  if (!partners.value.length) {
    partnerStore.fetchPartners();
  }
});

const prev = () => {
  carousel.value?.prev(); // <-- method có sẵn
};

const next = () => {
  carousel.value?.next(); // <-- method có sẵn
};


</script>

<template>
  <div class="partner-carousel-wrapper">
    <!-- Tiêu đề + nút điều hướng -->
    <div class="top-title">
      <h3 class="title">SHOP BY BRANDS</h3>
      <div class="custom-nav-buttons">
        <button @click="prev" class="custom-nav-btn">
          <!-- Left arrow SVG -->
          <svg width="16" height="16" viewBox="0 0 24 24" fill="none">
            <path
              d="M15 18l-6-6 6-6"
              stroke="white"
              stroke-width="2"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
          </svg>
        </button>
        <button @click="next" class="custom-nav-btn">
          <!-- Right arrow SVG -->
          <svg width="16" height="16" viewBox="0 0 24 24" fill="none">
            <path
              d="M9 6l6 6-6 6"
              stroke="white"
              stroke-width="2"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
          </svg>
        </button>
      </div>
    </div>

    <!-- Carousel -->
    <Carousel
      v-if="partners.length"
      ref="carousel"
      class="carousel"
      :items-to-show="5"
      :items-to-scroll="1"
      :snap-align="'start'"
      :wrap-around="false"
      :pause-autoplay-on-hover="true"
    >
      <Slide
        v-for="partner in partners.filter((p) => p.isActive).slice(0, 8)"
        :key="partner.id"
      >
        <div
          class="slide-item"
          @click="$router.push(`/category/${partner.slug}`)"
        >
          <div class="image-wrapper">
            <img
              :src="partner.imgDefaultUrl"
              :alt="partner.name"
              class="img-default"
            />
            <img
              :src="partner.imgHoverUrl"
              :alt="partner.name"
              class="img-hover"
            />
          </div>
          <div class="item-name">{{ partner.name.toUpperCase() }} →</div>
        </div>
      </Slide>
    </Carousel>
  </div>
</template>

<style scoped>
.partner-carousel-wrapper {
  width: 100%;
}

.carousel {
  margin: 0 2rem;
}

.top-title {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 0 2rem;
}

.title {
  font-size: 1.5rem;
  font-weight: 700;
  margin-top: 0;
  margin-bottom: 2rem ;
}

/* Custom navigation buttons */
.custom-nav-buttons {
  display: flex;
  gap: 12px;
}

.custom-nav-btn {
  background-color: rgba(0, 0, 0, 0.4);
  color: white;
  border: none;
  border-radius: 50%;
  width: 36px;
  height: 36px;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.custom-nav-btn:hover {
  background-color: rgba(0, 0, 0, 0.6);
}

/* Carousel slide item */
.slide-item {
  cursor: pointer;
}

.slide-item img {
  width: 280px;
  height: 250px;
  box-shadow: 0 3px 8px rgba(0, 0, 0, 0.15);
}
.image-default {
  background-size: cover;
}

.img-hover {
  background-size: contain;
  display: none;
}

.slide-item:hover .img-default {
  display: none;
}

.slide-item:hover .img-hover {
  display: block;
}

.item-name {
  margin-top: 8px;
  font-weight: 600;
  font-size: 14px;
}
</style>
