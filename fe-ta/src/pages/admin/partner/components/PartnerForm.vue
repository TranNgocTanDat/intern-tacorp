<script lang="ts" setup>
import { reactive, watch } from "vue";
import type { PartnerRequest, PartnerResponse } from "@/models/Partner";

const props = defineProps<{
  initialData?: PartnerResponse | null;
  mode?: "create" | "update";
  visible?: boolean;
}>();

const emit = defineEmits<{
  (e: "submit-form", request: PartnerRequest): void;
  (e: "update:visible", value: boolean): void;
}>();

// 👉 Form reactive
const form = reactive<PartnerRequest>({
  name: "",
  logoFile: null,
  imgDefaultFile: null,
  imgHoverFile: null,
  slug: "",
  link: "",
  orderIndex: 0,
  isActive: true,
  note: "",
});

// 👉 Reset form khi tạo mới
function resetForm() {
  form.name = "";
  form.logoFile = null;
  form.imgDefaultFile = null;
  form.imgHoverFile = null;
  form.slug = "";
  form.link = "";
  form.orderIndex = 0;
  form.isActive = true;
  form.note = "";
}

// 👉 Gán dữ liệu khi update
function setFormFromResponse(data: PartnerResponse) {
  form.name = data.name ?? "";
  form.link = data.link ?? "";
  form.orderIndex = data.orderIndex ?? 0;
  form.isActive = data.isActive ?? true;
  form.note = data.note ?? "";
  form.logoFile = null; // reset file upload (chỉ dùng để gửi mới)
  form.imgDefaultFile = null;
  form.imgHoverFile = null;
  form.slug = data.slug ?? "";
}

// 👉 Theo dõi khi prop thay đổi (mode: update)
watch(
  () => props.initialData,
  (val) => {
    if (val) {
      setFormFromResponse(val);
    } else {
      resetForm();
    }
  },
  { immediate: true }
);

// 👉 Submit form
const onSubmit = () => {
  emit("submit-form", {
    name: form.name,
    logoFile: form.logoFile,
    imgDefaultFile: form.imgDefaultFile,
    imgHoverFile: form.imgHoverFile,
    slug: form.slug,
    link: form.link,
    orderIndex: form.orderIndex,
    isActive: form.isActive,
    note: form.note,
  });

  // Reset sau khi thêm mới
  if (props.mode === "create") resetForm();

  emit("update:visible", false);
};
</script>

<template>
  <el-dialog
    :title="props.mode === 'create' ? 'Thêm mới đối tác' : 'Chỉnh sửa đối tác'"
    :model-value="props.visible"
    width="600px"
    @close="emit('update:visible', false)"
  >
    <el-form :model="form" label-width="120px" label-position="top">
      <el-form-item label="Tên đối tác">
        <el-input v-model="form.name" placeholder="Nhập tên đối tác..." />
      </el-form-item>

      <el-form-item label="Logo đối tác">
        <input
          type="file"
          accept="image/*"
          @change="
            (e) =>
              (form.logoFile =
                (e.target as HTMLInputElement)?.files?.[0] ?? null)
          "
        />
        <div v-if="props.initialData?.logoUrl">
          <p class="text-sm mt-2">Logo hiện tại:</p>
          <img
            :src="props.initialData.logoUrl"
            alt="Logo"
            style="max-width: 60px; max-height: 60px; margin-top: 4px"
          />
        </div>
      </el-form-item>
      <el-form-item label="Ảnh mặc định">
        <input
          type="file"
          accept="image/*"
          @change="
            (e) =>
              (form.imgDefaultFile =
                (e.target as HTMLInputElement)?.files?.[0] ?? null)
          "
        />
        <div v-if="props.initialData?.imgDefaultUrl">
          <p class="text-sm mt-2">Ảnh mặc định:</p>
          <img
            :src="props.initialData.imgDefaultUrl"
            alt="Logo"
            style="max-width: 60px; max-height: 60px; margin-top: 4px"
          />
        </div>
      </el-form-item>
      <el-form-item label="Ảnh hover">
        <input
          type="file"
          accept="image/*"
          @change="
            (e) =>
              (form.imgHoverFile =
                (e.target as HTMLInputElement)?.files?.[0] ?? null)
          "
        />
        <div v-if="props.initialData?.imgHoverUrl">
          <p class="text-sm mt-2">Ảnh Hover:</p>
          <img
            :src="props.initialData.imgHoverUrl"
            alt="Logo"
            style="max-width: 60px; max-height: 60px; margin-top: 4px"
          />
        </div>
      </el-form-item>
      <el-form-item label="Slug">
        <el-input
          v-model="form.slug"
          placeholder="VD: apple,samsung,..."
        />
      </el-form-item>

      <el-form-item label="Liên kết (Link)">
        <el-input
          v-model="form.link"
          placeholder="VD: https://www.example.com"
        />
      </el-form-item>

      <el-form-item label="Thứ tự hiển thị">
        <el-input-number v-model="form.orderIndex" :min="0" />
      </el-form-item>

      <el-form-item label="Trạng thái hoạt động">
        <el-switch
          v-model="form.isActive"
          active-text="Hoạt động"
          inactive-text="Ngưng"
        />
      </el-form-item>

      <el-form-item label="Ghi chú">
        <el-input
          v-model="form.note"
          type="textarea"
          placeholder="Nhập ghi chú..."
        />
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
