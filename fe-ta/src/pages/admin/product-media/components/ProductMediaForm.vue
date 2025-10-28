<script lang="ts" setup>
import type { ProductResponse } from "@/models/Product";
import type {
  ProductMediaRequest,
  ProductMediaResponse,
} from "@/models/ProductMedia";
import { reactive, ref, watch } from "vue";
import { useProductColorStore } from "@/store/productColorStore";
import type { ProductColorResponse } from "@/models/ProductColor";

const props = defineProps<{
  products: ProductResponse[];
  initialData?: ProductMediaResponse | null;
  mode?: "create" | "update";
  visible?: boolean;
}>();

const productColorStore = useProductColorStore();
const productColorsByProduct = ref<ProductColorResponse[]>([]);

const selectedProductId = ref<number | null>(null);

watch(selectedProductId, async (newProductId) => {
  form.colorId = undefined; // Reset màu

  if (newProductId) {
    await productColorStore.getProductColorByProductId(newProductId);
    productColorsByProduct.value = productColorStore.productColors;
  } else {
    productColorsByProduct.value = [];
  }
});

const emit = defineEmits<{
  (e: "submit-form", productId: number, request: ProductMediaRequest): void;
  (e: "update:visible", value: boolean): void;
}>();

// form state
const form = reactive<ProductMediaRequest>({
  mediaFileUrl: null as File | null,
  colorId: undefined,
  mediaType: "",
  descriptionMedia: "",
  isPrimary: false,
  orderIndex: 0,
  note: "",
});

// reset form
function resetForm() {
  form.mediaFileUrl = null;
  form.colorId = undefined;
  form.mediaType = "";
  form.descriptionMedia = "";
  form.isPrimary = false;
  form.orderIndex = 0;
  form.note = "";
}

// set form data when editing
function setFormFromResponse(data: ProductMediaResponse) {
  form.mediaFileUrl = null;
  form.mediaType = data.mediaType ?? "";
  form.descriptionMedia = data.descriptionMedia ?? "";
  form.colorId = data.colorId ?? undefined;
  form.isPrimary = data.isPrimary ?? false;
  form.orderIndex = data.orderIndex ?? 0;

  form.note = data.note ?? "";
}

// auto fill when initialData changes
watch(
  () => props.initialData,
  async (val) => {
    if (val) {
      setFormFromResponse(val);
      selectedProductId.value = val.productId ?? null;

      if (val.productId) {
        await productColorStore.getProductColorByProductId(val.productId);
        productColorsByProduct.value = productColorStore.productColors;
      }
    } else {
      resetForm();
      productColorsByProduct.value = [];
    }
  },
  { immediate: true }
);

// submit
const onSubmit = () => {
  if (!selectedProductId.value) {
    console.warn("ProductId is required");
    return;
  }

  const payload: ProductMediaRequest = {
    colorId: form.colorId,
    mediaFileUrl: form.mediaFileUrl,
    mediaType: form.mediaType,
    descriptionMedia: form.descriptionMedia,
    isPrimary: form.isPrimary,
    orderIndex: form.orderIndex,

    note: form.note,
  };

  emit("submit-form", selectedProductId.value, payload);

  if (props.mode === "create") resetForm();
};

const mediaTypeOptions = [
  { label: "Ảnh chính", value: "main" },
  { label: "Video", value: "video" },
  { label: "Ảnh mô tả", value: "description" },
];
</script>

<template>
  <el-dialog
    :title="props.mode === 'create' ? 'Thêm media' : 'Chỉnh sửa media'"
    :model-value="props.visible"
    width="700px"
    @close="emit('update:visible', false)"
  >
    <el-form :model="form" label-width="120px" label-position="top">
      <el-form-item label="Sản phẩm">
        <el-select
          v-model="selectedProductId"
          placeholder="Chọn hoặc nhập sản phẩm"
          clearable
          filterable
          allow-create
          default-first-option
          :teleported="false"
        >
          <el-option
            v-for="p in products"
            :key="p.id"
            :label="p.productName"
            :value="p.id"
          />
        </el-select>
      </el-form-item>

      <el-form-item label="Màu sắc">
        <el-select
          v-model="form.colorId"
          placeholder="Chọn hoặc nhập màu"
          clearable
          filterable
          allow-create
          default-first-option
          :teleported="false"
        >
          <el-option
            v-for="c in productColorsByProduct"
            :key="c.id"
            :label="c.colorName"
            :value="c.id"
          />
        </el-select>
      </el-form-item>

      <el-form-item label="Ảnh media">
        <input
          type="file"
          accept="image/*"
          @change="(e) => form.mediaFileUrl = (e.target as HTMLInputElement)?.files?.[0] ?? null"
        />
        <div v-if="props.initialData?.mediaFileUrl">
          <p class="text-sm mt-2">Ảnh hiện tại:</p>
          <img
            :src="props.initialData.mediaFileUrl"
            alt="Media Image"
            style="max-width: 50px; max-height: 50px; margin-top: 4px"
          />
        </div>
      </el-form-item>
      <el-form-item label="Loại media">
        <el-select
          v-model="form.mediaType"
          placeholder="Chọn loại media"
          default-first-option
          :teleported="false"
        >
          <el-option
            v-for="item in mediaTypeOptions"
            :key="item.value"
            :label="item.label"
            :value="item.value"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="Mô tả media">
        <el-input v-model="form.descriptionMedia" type="textarea" />
      </el-form-item>
      <el-form-item label="Anh đại diện">
        <el-switch v-model="form.isPrimary" />
      </el-form-item>
      <el-form-item label="Thứ tự hiển thị">
        <el-input-number v-model="form.orderIndex" :min="0" />
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
