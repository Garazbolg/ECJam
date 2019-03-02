shader_type canvas_item;
render_mode blend_mix;

void fragment() {
	COLOR = texture(TEXTURE, UV);
	COLOR.a = length(COLOR.rgb);
}