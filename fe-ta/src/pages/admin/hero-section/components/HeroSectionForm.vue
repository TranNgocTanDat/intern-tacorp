<script lang="ts" setup>
import type {
  HeroSectionRequest,
  HeroSectionResponse,
} from "@/models/HeroSection";
import { reactive, watch } from "vue";

const props = defineProps<{
  initialData?: HeroSectionResponse | null;
  mode?: "create" | "update";
  visible?: boolean;
}>();

const emit = defineEmits<{
  (e: "submit-form", request: HeroSectionRequest): void;
  (e: "update:visible", value: boolean): void;
}>();

// do not use same name with ref
const form = reactive<HeroSectionRequest>({
  title: "",
  description: "",
  isPublished: false,
  publishFrom: "",
  publishTo: "",
  heroMediaFile: null as File | null,
  pageHero: "",
});

// 👉 Hàm reset mặc định (create mode)
function resetForm() {
  form.title = "";
  form.description = "";
  form.isPublished = false;
  form.publishFrom = "";
  form.publishTo = "";
  form.heroMediaFile = null;
  form.pageHero = "";
}

// 👉 Hàm nạp dữ liệu từ response (update mode)
function setFormFromResponse(data: HeroSectionResponse) {
  form.title = data.title ?? "";
  form.description = data.description ?? "";
  form.isPublished = data.isPublished;
  form.publishFrom = data.publishFrom ?? "";
  form.publishTo = data.publishTo ?? "";
  form.pageHero = data.pageHero ?? "";
  form.heroMediaFile = null; // reset, vì response không có File object
 
}

// Nạp dữ liệu khi có initialData (edit/update)
watch(
  () => props.initialData,
  (val) => {
    if (val) {
      setFormFromResponse(val);
    } else {
      // reset form when initialData is null
      resetForm();
    }
  },
  { immediate: true }
);

const onSubmit = () => {
  emit("submit-form", {
    title: form.title,
    description: form.description,
    isPublished: form.isPublished,
    publishFrom: form.publishFrom,
    publishTo: form.publishTo,
    heroMediaFile: form.heroMediaFile,
    pageHero: form.pageHero,
  });
  if (props.mode === "create") {
    // Reset form only in create mode
    form.title = "";
    form.description = "";
    form.isPublished = false;
    form.publishFrom = "";
    form.publishTo = "";
    form.pageHero = "";
    form.heroMediaFile = null;
  }
  emit("update:visible", false);
};
</script>

<template>
  <el-dialog
    :title="
      props.mode === 'create'
        ? 'Thêm mới Hero Section'
        : 'Chỉnh sửa Hero Section'
    "
    :model-value="props.visible"
    width="600px"
    @close="emit('update:visible', false)"
  >
    <el-form :model="form" label-width="120px" label-position="top">
      <el-form-item label="Tiêu đề">
        <el-input v-model="form.title" placeholder="Nhập tiêu đề..." />
      </el-form-item>

      <el-form-item label="Mô tả">
        <el-input
          v-model="form.description"
          type="textarea"
          placeholder="Nhập mô tả..."
        />
      </el-form-item>

      <el-form-item label="Ngày bắt đầu">
        <el-date-picker
          v-model="form.publishFrom"
          type="datetime"
          placeholder="Chọn ngày bắt đầu"
          style="width: 100%"
          :teleported="false"
        />
      </el-form-item>

      <el-form-item label="Ngày kết thúc">
        <el-date-picker
          v-model="form.publishTo"
          type="datetime"
          placeholder="Chọn ngày kết thúc"
          style="width: 100%"
          :teleported="false"
        />
      </el-form-item>

      <el-form-item label="Hiển thị ngay">
        <el-switch
          v-model="form.isPublished"
          active-text="Có"
          inactive-text="Không"
        />
      </el-form-item>

      <el-form-item label="Ảnh nền (Hero Media)">
        <input
          type="file"
          accept="image/*"
          @change="(e) => form.heroMediaFile = (e.target as HTMLInputElement)?.files?.[0] ?? null"
        />
        <div v-if="props.initialData?.heroMediaUrl">
          <p class="text-sm mt-2">Ảnh hiện tại:</p>
          <img
            :src="props.initialData.heroMediaUrl"
            alt="Hero Image"
            style="max-width: 50px; max-height: 50px; margin-top: 4px"
          />
        </div>
      </el-form-item>

      <el-form-item label="Trang áp dụng">
        <el-input v-model="form.pageHero" placeholder="VD: /home, /about" />
      </el-form-item>

      <el-form-item>
        <el-button @click="emit('update:visible', false)">Hủy</el-button>
        <el-button type="primary" @click="onSubmit">
          {{ props.mode === "create" ? "Thêm mới" : "Lưu thay đổi" }}
        </el-button>
      </el-form-item>
    </el-form>
  </el-dialog>
</template>
