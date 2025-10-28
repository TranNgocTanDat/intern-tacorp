<script lang="ts" setup>
import type { HeroSectionResponse } from "@/models/HeroSection";
import type {
  HeroSectionProductRequest,
  HeroSectionProductResponse,
} from "@/models/HeroSectionProduct";
import type { ProductResponse } from "@/models/Product";
import { reactive, watch } from "vue";

// tạo props truyền dữ liệu từ component cha
const props = defineProps<{
  initialData?: HeroSectionProductResponse | null;
  heroSections: HeroSectionResponse[];
  products: ProductResponse[];
  mode?: "create" | "update";
  visible?: boolean;
}>();

// tạo emit để truyền sự kiện về component cha
const emit = defineEmits<{
  (e: "submit-form", request: HeroSectionProductRequest): void;
  (e: "update:visible", value: boolean): void;
}>();

// tạo đối tượng form với reactive để lắng nghe thay đổi dữ liệu
const form = reactive({
  heroSectionId: null as number | null,
  productId: null as number | null,
  orderIndex: 0,
});

// Theo dõi sự thay đổi của props.initialData để cập nhật lại form
watch(
  () => props.initialData,
  (val) => {
    if (val) {
      form.heroSectionId = val.heroSection?.id ?? null;
      form.productId = val.product?.id ?? null;
      form.orderIndex = val.orderIndex ?? 0;
    } else {
      // reset form when initialData is null
      form.heroSectionId = null;
      form.productId = null;
      form.orderIndex = 0;
    }
  },
  { immediate: true }
);

// Xử lý sự kiện submit form
const onSubmit = () => {
  emit("submit-form", {
    heroSectionId: form.heroSectionId!,
    productId: form.productId!,
    orderIndex: form.orderIndex,
  });
  if (props.mode === "create") {
    // reset form after submit if in create mode
    form.heroSectionId = null;
    form.productId = null;
    form.orderIndex = 0;
  } else {
    // close form if in update mode
    emit("update:visible", false);
  }
};
</script>

<template>
  <el-dialog
    :title="
      props.mode === 'create'
        ? 'Thêm mới HeroSectionProduct'
        : 'Cập nhật HeroSectionProduct'
    "
    :model-value="props.visible"
    width="600px"
    @close="emit('update:visible', false)"
  >
    <el-form :model="form" label-width="120px">
      <el-form-item label="Hero Section">
        <el-select
          v-model="form.heroSectionId"
          placeholder="Chọn Hero Section"
          :teleported="false"
        >
          <el-option label="Test Section" :value="999" />
          <el-option
            v-for="section in props.heroSections"
            :key="section.id"
            :label="`${section.title}`"
            :value="section.id"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="Product">
        <el-select
          v-model="form.productId"
          placeholder="Chọn Product"
          :teleported="false"
        >
          <el-option
            v-for="product in props.products"
            :key="product.id"
            :label="`${product.productName}`"
            :value="product.id"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="Order Index">
        <el-input v-model.number="form.orderIndex" type="number" />
      </el-form-item>
    </el-form>
    <template #footer>
      <span class="dialog-footer">
        <el-button @click="emit('update:visible', false)">Hủy</el-button>
        <el-button type="primary" @click="onSubmit">
          {{ props.mode === "create" ? "Tạo" : "Cập nhật" }}
        </el-button>
      </span>
    </template>
  </el-dialog>
</template>

<style lang="css" scoped></style>
