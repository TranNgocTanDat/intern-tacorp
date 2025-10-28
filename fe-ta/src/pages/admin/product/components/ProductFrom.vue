<script lang="ts" setup>
import type { CategoryResponse } from "@/models/Category";
import type { ProductRequest, ProductResponse } from "@/models/Product";
import { reactive, watch } from "vue";

const props = defineProps<{
  categoriesChild: CategoryResponse[];
  initialData?: ProductResponse | null;
  mode?: "create" | "update";
  visible?: boolean;
}>();

const emit = defineEmits<{
  (e: "submit-form", request: ProductRequest): void;
  (e: "update:visible", value: boolean): void;
}>();

// form state
const form = reactive<ProductRequest>({
  productName: "",
  originalPrice: 0,
  discountPrice: 0,
  slug: "",
  isFeatured: false,
  categoryId: undefined,
  isActive: true,
  shortDescription: "",
  longDescription: "",
  note: "",
});

// reset form
function resetForm() {
  form.productName = "";
  form.originalPrice = 0;
  form.discountPrice = 0;
  form.slug = "";
  form.isFeatured = false;
  form.categoryId = undefined;
  form.isActive = true;
  form.shortDescription = "";
  form.longDescription = "";
  form.note = "";
}

// set form data when editing
function setFormFromResponse(data: ProductResponse) {
  form.productName = data.productName ?? "";
  form.originalPrice = data.originalPrice ?? 0;
  form.discountPrice = data.discountPrice ?? 0;
  form.slug = data.slug ?? "";
  form.isFeatured = data.isFeatured ?? false;
  form.categoryId = data.categoryId ?? undefined;
  form.isActive = data.isActive ?? true;
  form.shortDescription = data.shortDescription ?? "";
  form.longDescription = data.longDescription ?? "";
  form.note = data.note ?? "";
}

// auto fill when initialData changes
watch(
  () => props.initialData,
  (val) => {
    if (val) setFormFromResponse(val);
    else resetForm();
  },
  { immediate: true }
);

// submit
const onSubmit = () => {
  const payload: ProductRequest = {
    productName: form.productName,
    originalPrice: form.originalPrice,
    discountPrice: form.discountPrice,
    slug: form.slug,
    isFeatured: form.isFeatured,
    categoryId: form.categoryId,
    isActive: form.isActive,
    shortDescription: form.shortDescription,
    longDescription: form.longDescription,
    note: form.note,
  };

  emit("submit-form", payload);

  if (props.mode === "create") resetForm();
};
</script>

<template>
  <el-dialog
    :title="props.mode === 'create' ? 'Thêm sản phẩm' : 'Chỉnh sửa sản phẩm'"
    :model-value="props.visible"
    width="700px"
    @close="emit('update:visible', false)"
  >
    <el-form :model="form" label-width="120px" label-position="top">
      <el-form-item label="Tên sản phẩm">
        <el-input v-model="form.productName" placeholder="Nhập tên..." />
      </el-form-item>

      <el-form-item label="Slug">
        <el-input
          v-model="form.slug"
          placeholder="Slug tự động hoặc tùy chỉnh"
        />
      </el-form-item>

      <el-form-item label="Giá gốc">
        <el-input-number v-model="form.originalPrice" :min="0" />
      </el-form-item>
      <el-form-item label="Giá sale">
        <el-input-number v-model="form.discountPrice" :min="0" />
      </el-form-item>

      <el-form-item label="Danh mục">
        <el-select
          v-model="form.categoryId"
          placeholder="Chọn danh mục"
          clearable
          :teleported="false"
        >
          <el-option
            v-for="cat in categoriesChild"
            :key="cat.id"
            :label="cat.name"
            :value="cat.id"
          />
        </el-select>
      </el-form-item>

      <el-form-item label="Mô tả ngắn">
        <el-input v-model="form.shortDescription" type="textarea" />
      </el-form-item>

      <el-form-item label="Mô tả dài">
        <el-input v-model="form.longDescription" type="textarea" />
      </el-form-item>

      <el-form-item label="Nổi bật">
        <el-switch v-model="form.isFeatured" />
      </el-form-item>

      <el-form-item label="Kích hoạt">
        <el-switch v-model="form.isActive" />
      </el-form-item>
    </el-form>

    <template #footer>
      <el-button @click="emit('update:visible', false)">Hủy</el-button>
      <el-button type="primary" @click="onSubmit">
        {{ props.mode === "create" ? "Tạo" : "Cập nhật" }}
      </el-button>
    </template>
  </el-dialog>
</template>
